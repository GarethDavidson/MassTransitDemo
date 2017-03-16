using MassTransit;
using Messaging.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace TestSubscriber
{
    class MessageLogConsumer : IConsumer<IHelloWorld>
    {
        private ISubmitMessage<IHelloWorld> MessageService;

        public MessageLogConsumer(ISubmitMessage<IHelloWorld> messageService)
        {
            MessageService = messageService;
        }

        public  async Task Consume(ConsumeContext<IHelloWorld> context)
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
