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
                
                  // Creating new endpoints here create new queues
                  x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", ep =>
                   {
                       ep.Consumer<ScheduleNotificationConsumer>();
                   });

                  x.ReceiveEndpoint(host, "message_log", e =>
                  {
                      e.Consumer(() => new MessageLogConsumer(new SubmitMessage<IHelloWorld>("http://localhost:56045", "api/Hello")));
                  });

                  x.ReceiveEndpoint(host, "message_log2", e =>
                  {
                      e.Consumer(() => new MessageLogConsumer(new SubmitMessage<IHelloWorld>("http://localhost:56045", "api/Hello")));
                  });
              });

            bus.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
           
        }
    }
}
