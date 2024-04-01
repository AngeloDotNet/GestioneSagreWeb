using GestioneSagre.Messaging.Models.InputModels;
using GestioneSagre.Messaging.Models.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.Categorie.MessageBroker.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitOptions = configuration.GetSection("RabbitMQ").Get<RabbitProducerOptions>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.QueueExpiration = TimeSpan.FromSeconds(rabbitOptions.QueueExpiration);

                cfg.Host(rabbitOptions.Host, rabbitOptions.VirtualHost, h =>
                {
                    h.Username(rabbitOptions.Username);
                    h.Password(rabbitOptions.Password);
                });

                cfg.ReceiveEndpoint(rabbitOptions.ReceivedEndpoint, e =>
                {
                    e.Durable = rabbitOptions.Durable;
                    e.AutoDelete = rabbitOptions.AutoDelete;
                    e.ExchangeType = rabbitOptions.ExchangeType;
                    e.PrefetchCount = rabbitOptions.PrefetchCount;

                    e.UseMessageRetry(r => r.Interval(rabbitOptions.RetryCount, rabbitOptions.RetryInterval));
                });
            });

            x.AddRequestClient(typeof(RequestFestaAttiva));
            //x.AddRequestClient<RequestFestaAttiva>();
        });

        return services;
    }
}