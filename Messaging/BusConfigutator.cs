using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace Messaging
{
    public static class BusConfigutator
    {
        public static IBusControl ConfigureBus(RabbitMqConstants rabbitConfig,
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> 
            registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(rabbitConfig.RabbitMqUri), hst =>
                {
                    hst.Username(rabbitConfig.UserName);
                    hst.Password(rabbitConfig.Password);
                    hst.PublisherConfirmation = rabbitConfig.PublisherConfirm;
                });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
