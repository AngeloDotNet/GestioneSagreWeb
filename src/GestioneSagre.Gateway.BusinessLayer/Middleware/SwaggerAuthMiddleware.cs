using System.Net.Http.Headers;
using System.Text;
using GestioneSagre.Gateway.BusinessLayer.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace GestioneSagre.Gateway.BusinessLayer.Middleware;

public class SwaggerAuthMiddleware
{
    private readonly RequestDelegate next;
    private readonly IOptionsMonitor<SwaggerSettings> swaggerOptions;

    public SwaggerAuthMiddleware(RequestDelegate next, IOptionsMonitor<SwaggerSettings> swaggerOptions)
    {
        this.next = next;
        this.swaggerOptions = swaggerOptions;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var settings = swaggerOptions.CurrentValue;

        if (context.Request.Path.StartsWithSegments("/swagger") && !string.IsNullOrEmpty(settings.UserName) && !string.IsNullOrEmpty(settings.Password))
        {
            string authenticationHeader = context.Request.Headers[HeaderNames.Authorization];

            if (authenticationHeader?.StartsWith("Basic ") ?? false)
            {
                var header = AuthenticationHeaderValue.Parse(authenticationHeader);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(header.Parameter)).Split(':', count: 2);
                var userName = credentials.ElementAtOrDefault(0);
                var password = credentials.ElementAtOrDefault(1);

                if (userName == settings.UserName && password == settings.Password)
                {
                    await next.Invoke(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.WWWAuthenticate = new StringValues("Basic");
        }
        else
        {
            await next.Invoke(context);
        }
    }
}