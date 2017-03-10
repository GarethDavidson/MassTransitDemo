using Messaging.Models.Interface;
using System;

namespace Messaging.Models
{
    public class SomethingHappened : ISomethingHappened
    {
        public string What { get; set; }
        public DateTime When { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}
