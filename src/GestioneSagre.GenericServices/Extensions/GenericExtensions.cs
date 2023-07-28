using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.GenericServices.Extensions;

public static class GenericExtensions
{
    public static IServiceCollection AddPolicyCors(this IServiceCollection services, string policyName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy($"{policyName}", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        return services;
    }
}