using MassTransit;
using Messaging.Models;
using Messaging.Models.Interface;
using Messaging.Models.Interfaces;
using Quartz;
using Quartz.Impl;
using System;
using Topshelf;

namespace TestPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // Change to UseMessageScheduler with the Uri of the running scheduler
                // Azure Comes with its own Scheduler,
                // MassTransit comes with Quartz.net, which is currently being used. 
                x.UseInMemoryScheduler();

            });

            HostFactory.Run(x =>
            {
                x.Service<ServiceController>(s =>
                {
                    s.ConstructUsing(name => new ServiceController(bus));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.StartAutomatically();

                x.RunAsLocalSystem();
            });

        }

    }

    public class ServiceController
    {
        IBusControl busControl;
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

        public ServiceController(IBusControl busControl)
        {
            this.busControl = busControl;
        }

        public async void Start()
        {
            string text = "";

            await busControl.StartAsync();

            while (text != "quit")
            {
                Console.WriteLine("Enter a message: ");

                text = Console.ReadLine();

                DateTime TimeToSend = DateTime.Now;

                bool HelloWorlds = false;

                switch (text.ToLower())
                {
                    case "now":
                        TimeToSend = DateTime.Now;
                        break;
                    case "in2":
                        TimeToSend = DateTime.Now.AddSeconds(2);
                        break;
                    case "in5":
                        TimeToSend = DateTime.Now.AddSeconds(5);
                        break;
                    case "in10":
                        TimeToSend = DateTime.Now.AddSeconds(10);
                        break;
                    case "1minute":
                        TimeToSend = DateTime.Now.AddMinutes(1);
                        break;
                    case "hello":
                        HelloWorlds = true;
                        break;
                    default:
                        {
                            System.Console.WriteLine("Other Input");
                            break;
                        }
                }

                if (HelloWorlds)
                {
                    // This Message does not use the scheduler.
                    // It will be pulished to a Subscriber that can handle this model, it will send a http request to the Consumer Controller route
                    var message_log = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/message_log"));

                    message_log.Send<IHelloWorld>(new
                    {
                        MessageSend = "Hello World"
                    }).Wait();
                }
                else {
                    if (TimeToSend > DateTime.Now)
                    {
                        var scheduleEndPoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/quartz"));

                        await scheduleEndPoint.ScheduleSend<ISomethingHappened>(
                            new Uri("rabbitmq://localhost/MtPubSubExample_TestSubscriber"),
                           TimeToSend,
                            new { What = text, When = DateTime.Now, DeliveryTime = TimeToSend });
                    }
                    else
                    {
                        var MtPubSubExample_TestSubscriber = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/MtPubSubExample_TestSubscriber"));

                        MtPubSubExample_TestSubscriber.Send<ISomethingHappened>(
                            new { What = text + " Now", When = DateTime.Now, DeliveryTime = TimeToSend }
                            ).Wait();
                    }
                }
            }
        }

        public void Stop()
        {
            busControl.StopAsync();
        }
    }
}
