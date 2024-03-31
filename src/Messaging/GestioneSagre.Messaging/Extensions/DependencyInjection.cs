using GestioneSagre.Messaging.Models.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.Messaging.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMQSender<Request>(this IServiceCollection services, RabbitOptions rabbitOptions)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                //cfg.QueueExpiration = TimeSpan.FromSeconds(rabbitOptions.QueueExpiration);
                cfg.QueueExpiration = TimeSpan.FromSeconds(RabbitStaticConfig.queueExpiration);

                cfg.Host(rabbitOptions.Host, rabbitOptions.VirtualHost, h =>
                {
                    h.Username(rabbitOptions.Username);
                    h.Password(rabbitOptions.Password);
                });

                cfg.ReceiveEndpoint(rabbitOptions.ReceivedEndpoint, e =>
                {
                    e.Durable = RabbitStaticConfig.durable; //rabbitOptions.Durable;
                    e.AutoDelete = RabbitStaticConfig.autoDelete; //rabbitOptions.AutoDelete;
                    e.ExchangeType = RabbitStaticConfig.exchangeType; //rabbitOptions.ExchangeType;
                    e.PrefetchCount = RabbitStaticConfig.prefetchCount; //rabbitOptions.PrefetchCount;

                    //e.UseMessageRetry(r => r.Interval(rabbitOptions.RetryCount, rabbitOptions.RetryInterval));
                    e.UseMessageRetry(r => r.Interval(RabbitStaticConfig.retryCount, RabbitStaticConfig.interval));
                });
            });

            x.AddRequestClient(typeof(Request));
        });

        return services;
    }

    public static IServiceCollection AddRabbitMQReceiver<Consumer>(this IServiceCollection services, RabbitOptions rabbitOptions)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(Consumer).Assembly);
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitOptions.Host, rabbitOptions.VirtualHost, h =>
                {
                    h.Username(rabbitOptions.Username);
                    h.Password(rabbitOptions.Password);
                });

                cfg.ReceiveEndpoint(rabbitOptions.ReceivedEndpoint, e =>
                {
                    e.Durable = RabbitStaticConfig.durable; //rabbitOptions.Durable;
                    e.AutoDelete = RabbitStaticConfig.autoDelete; //rabbitOptions.AutoDelete;
                    e.ExchangeType = RabbitStaticConfig.exchangeType; //rabbitOptions.ExchangeType;
                    e.PrefetchCount = RabbitStaticConfig.prefetchCount; //rabbitOptions.PrefetchCount;

                    e.ConfigureConsumers(context);
                });
            });
        });

        return services;
    }
}
