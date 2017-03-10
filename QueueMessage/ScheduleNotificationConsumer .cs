using MassTransit;
using Messaging.Models;
using System;
using System.Threading.Tasks;

public class ScheduleNotificationConsumer :
    IConsumer<SomethingHappened>
{
    public async Task Consume(ConsumeContext<SomethingHappened> context)
    {
        Console.WriteLine(
            "TXT: " + context.Message.What +
            ", When: " + context.Message.When.ToString() +
            ", DT: " + context.Message.DeliveryTime.ToString()
            );
    }
}