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
                cfg.QueueExpiration = TimeSpan.FromSeconds(RabbitStaticConfig.queueExpiration);

                cfg.Host(rabbitOptions.Host, rabbitOptions.VirtualHost, h =>
                {
                    h.Username(rabbitOptions.Username);
                    h.Password(rabbitOptions.Password);
                });

                cfg.ReceiveEndpoint(rabbitOptions.ReceivedEndpoint, e =>
                {
                    e.Durable = RabbitStaticConfig.durable;
                    e.AutoDelete = RabbitStaticConfig.autoDelete;
                    e.ExchangeType = RabbitStaticConfig.exchangeType;
                    e.PrefetchCount = RabbitStaticConfig.prefetchCount;
                    //e.Bind("TestExchangeSender");

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
                    e.Durable = RabbitStaticConfig.durable;
                    e.AutoDelete = RabbitStaticConfig.autoDelete;
                    e.ExchangeType = RabbitStaticConfig.exchangeType;
                    e.PrefetchCount = RabbitStaticConfig.prefetchCount;
                    //e.Bind("TestExchangeReceiver");

                    e.ConfigureConsumers(context);
                });
            });
        });

        return services;
    }
}
