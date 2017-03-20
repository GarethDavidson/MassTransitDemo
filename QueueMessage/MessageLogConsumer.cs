using MassTransit;
using Messaging.Models;
using System;
using System.Threading.Tasks;

namespace TestSubscriber
{
    class MessageLogConsumer : IConsumer<HelloWorld>
    {
        private ISubmitMessage<HelloWorld> MessageService;

        public MessageLogConsumer(ISubmitMessage<HelloWorld> messageService)
        {
            MessageService = messageService;
        }

        public  async Task Consume(ConsumeContext<HelloWorld> context)
        {
            try
            {
                var message = context.Message;
                await MessageService.Submit(message);
            }
            catch(Exception ex)
            {
                throw; // MassTransit will send errors to an error queue
            }
        }
    }
}
