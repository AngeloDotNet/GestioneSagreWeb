using GestioneSagre.OperazioniAvvio.MessageBroker.Models.InputModels;
using GestioneSagre.OperazioniAvvio.MessageBroker.Models.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.OperazioniAvvio.MessageBroker.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitOptions = configuration.GetSection("RabbitMQ").Get<RabbitOptions>();

        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(RequestFestaAttiva).Assembly);

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

                    e.ConfigureConsumers(context);
                });
            });
        });

        return services;
    }
}