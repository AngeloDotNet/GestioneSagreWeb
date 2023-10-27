using System.Diagnostics;
using GestioneSagre.DataProtection.BusinessLayer.Commands;
using GestioneSagre.DataProtection.DataAccessLayer;
using GestioneSagre.EFCore.Extensions;
using GestioneSagre.GenericServices.Extensions;
using GestioneSagre.Shared.OperationResults;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GestioneSagre.DataProtection.BusinessLayer.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddRegisterConfigureServices(this IServiceCollection services, IConfiguration configuration, string serviceName, string swaggerName)
    {
        var customServices = configuration.GetSection("PointAPI");
        var connectionSystem = configuration.GetSection("ConnectionStrings");
        var serverService = configuration.GetSection("Servers");

        var connStringSeq = serverService["Seq-Host"];
        var SeqApiKey = serverService["Seq-ApiKey"];
        var tagsAPI = new[] { "web api", "swagger" };
        var tagsSQLServer = new[] { "database", "sqlserver" };

        var connectionString = connectionSystem["TypeStartup"] switch
        {
            "Default" => connectionSystem["Default"],
            _ => connectionSystem["Docker"],
        };

        services.AddPolicyCors(serviceName);
        services.AddSerilogSeqServices();

        services.AddSwaggerGenConfig($"{swaggerName}", "v1");
        services.AddHealthChecks().AddSqlServer(connectionString, null, "DataBase Service", HealthStatus.Degraded, tagsSQLServer);
        services.AddCustomHealthChecks(customServices["Url"], customServices["Name"], tagsAPI, connStringSeq, SeqApiKey);

        services.AddDbContextSQLServer<ApplicationDbContext>(connectionString);
        services.AddTransient<IDataProtectionService, DataProtectionService>();

        services.AddDataProtection()
           .SetApplicationName("GestioneSagre")
           .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
           .PersistKeysToDbContext<ApplicationDbContext>();

        return services;
    }

    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails(options =>
        {
            options.ValidationProblemStatusCode = StatusCodes.Status422UnprocessableEntity;

            /* These configurations are optional and can be used to map specific exceptions to custom status codes. */
            options.Map<HttpRequestException>(ex => new StatusCodeProblemDetails(StatusCodes.Status503ServiceUnavailable));
            options.Map<ApplicationException>(ex => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest));
            //options.Map<Exception>(ex =>
            //{
            //    var error = new StatusCodeProblemDetails(StatusCodes.Status500InternalServerError)
            //    {
            //        Title = "Internal error",
            //        Detail = ex.Message
            //    };

            //    return error;
            //});
        }).AddProblemDetailsConventions();

        services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.Configure<ApiBehaviorOptions>(options =>
        {
            // Redefine the factory method that is used to create a 400 Bad Request response when Model validation fails.
            // In this example, the status code is replaced using 422 instead of 400.
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Any() ?? false)
                    .SelectMany(e => e.Value.Errors
                        .Select(x => new ValidationError(e.Key, x.ErrorMessage)));

                var httpContext = actionContext.HttpContext;
                var statusCode = StatusCodes.Status422UnprocessableEntity;
                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Type = $"https://httpstatuses.com/{statusCode}",
                    Instance = httpContext.Request.Path,
                    Title = "Validation errors occurred"
                };

                problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier);
                problemDetails.Extensions.Add("errors", errors);

                var result = new ObjectResult(problemDetails)
                {
                    StatusCode = statusCode
                };

                return result;
            };
        });

        return services;
    }

    public static WebApplication AddCustomConfigure(this WebApplication app, string swaggerName, string serviceName)
    {
        app.UseProblemDetails();
        app.UseSwaggerUINoRoutePrefix($"{swaggerName} v1");

        app.UseRouting();
        app.UseCors($"{serviceName}");

        app.AddSerilogConfigureServices();

        return app;
    }
}