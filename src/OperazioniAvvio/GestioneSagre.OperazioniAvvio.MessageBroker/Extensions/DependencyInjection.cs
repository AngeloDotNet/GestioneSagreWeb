using GestioneSagre.Messaging.Models.Options;
using GestioneSagre.OperazioniAvvio.MessageBroker.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.OperazioniAvvio.MessageBroker.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitOptions = configuration.GetSection("RabbitMQ").Get<RabbitConsumerOptions>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<ConsumerFestaAttiva>();

            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, rabbit) =>
            {
                rabbit.Host(rabbitOptions.Host, rabbitOptions.VirtualHost, h =>
                {
                    h.Username(rabbitOptions.Username);
                    h.Password(rabbitOptions.Password);
                });

                rabbit.ReceiveEndpoint(rabbitOptions.ReceivedEndpoint, e =>
                {
                    e.Durable = rabbitOptions.Durable;
                    e.AutoDelete = rabbitOptions.AutoDelete;
                    e.ExchangeType = rabbitOptions.ExchangeType;
                    e.PrefetchCount = rabbitOptions.PrefetchCount;

                    e.ConfigureConsumer<ConsumerFestaAttiva>(context);
                });
            });
        });

        return services;
    }
}