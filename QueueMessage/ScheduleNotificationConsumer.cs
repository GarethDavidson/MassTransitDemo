using MassTransit;
using Messaging.Models.Interface;
using System;
using System.Threading.Tasks;

public class ScheduleNotificationConsumer :
    IConsumer<ISomethingHappened>
{
    public async Task Consume(ConsumeContext<ISomethingHappened> context)
    {
        Console.WriteLine(
            "TXT: " + context.Message.What +
            ", When: " + context.Message.When.ToString() +
            ", Delivery Time: " + context.Message.DeliveryTime.ToString()
            );
    }
}