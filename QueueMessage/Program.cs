using MassTransit;
using Messaging.Models.Interfaces;
using Messaging.Models;
using System;

namespace TestSubscriber
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
                
                  x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", ep =>
                   {
                       ep.Consumer<ScheduleNotificationConsumer>();
                   });

                  x.ReceiveEndpoint(host, "message_log", e =>
                  {
                      e.Consumer(() => new MessageLogConsumer(new SubmitMessage<HelloWorld>("http://localhost:56045", "api/Hello")));
                  });
              });

            bus.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
           
        }
    }
}
