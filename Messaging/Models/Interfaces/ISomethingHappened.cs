using System;

namespace Messaging.Models.Interface
{
    public interface ISomethingHappened
    {
        string What { get; }
        DateTime When { get; }
        DateTime DeliveryTime { get; }
    }
}
