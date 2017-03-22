namespace Messaging
{
    public class RabbitMqConstants
    {
        private const string message_Log = "message_log";

        public RabbitMqConstants(string rabbitMqUri, string userName, string password, bool publisherConfirm)
        {
            RabbitMqUri = rabbitMqUri;
        }

        public string RabbitMqUri { get; }
        public string UserName { get; }
        public string Password { get; }
        public bool PublisherConfirm { get; }

        public string QueueName_MessageLog { get { return message_Log; } }
    }
}
