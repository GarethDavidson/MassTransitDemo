using MassTransit;
using Messaging;
using Messaging.Models.Interfaces;
using Messaging.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;
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

        public void Start()
        {
            string text = "";
            busControl.StartAsync();
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
                            System.Console.WriteLine("Other number");
                            break;
                        }
                }

                if (HelloWorlds)
                {
                    // This Message does note use the scheduler.
                    // It will be pulished to a Subscriber that will send a json request to the Consumer Controler route
                    busControl.Publish(new  HelloWorld()
                    {
                        MessageSend = "Hello World"
                    }).Wait();
                }
                else {
                    Task.Run(async () =>
                {
                    // 
                    var scheduleEndPoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/quartz"));

                    await scheduleEndPoint.ScheduleSend(
                        new Uri("rabbitmq://localhost/MtPubSubExample_TestSubscriber"),
                       TimeToSend,
                        new SomethingHappened() { What = text, When = DateTime.Now, DeliveryTime = TimeToSend });
                });
                }
            }
        }

        public void Stop()
        {
            busControl.StopAsync();
        }
    }
}
