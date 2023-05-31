using GestioneSagre.Gateway.BusinessLayer.Middleware;
using GestioneSagre.Gateway.BusinessLayer.Options;

namespace GestioneSagre.Gateway;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("GestioneSagre", policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddSerilogSeqServices();
        services.AddSwaggerGenConfig("Gestione Sagre Gateway", "v1");

        var jwtConfig = Configuration.GetSection("JwtSettings");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtConfig["AuthenticationScheme"], options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtConfig["Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecurityKey"])),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.Configure<SwaggerSettings>(Configuration.GetSection("SwaggerSettings"));
        services.AddOcelot(Configuration);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseCors("GestioneSagre");
        app.UseMiddleware<SwaggerAuthMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestione Sagre Gateway v1");
        });

        app.AddSerilogConfigureServices();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseOcelot().Wait();
    }
}