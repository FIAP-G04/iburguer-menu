using System.Diagnostics;
using iBurguer.Menu.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Menu.Infrastructure.WebApi;

public static class WebApiHostApplicationExtensions
{
    public static IHostApplicationBuilder AddWebApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddProblemDetails(options =>
            options.CustomizeProblemDetails = (context) =>
            {
                if (!context.ProblemDetails.Extensions.ContainsKey("traceId"))
                { 
                    string? traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
                    context.ProblemDetails.Extensions.Add(new KeyValuePair<string, object?>("traceId", traceId));
                }
            }
        );
        builder.AddSwagger();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .AllowAnyHeader();
            });
        });

        return builder;
    }
    
    public static WebApplication UseWebApi(this WebApplication app)
    {
        app.ConfigureSwagger();
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}

