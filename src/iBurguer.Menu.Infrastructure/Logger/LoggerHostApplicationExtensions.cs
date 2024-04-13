using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Elastic.CommonSchema.Serilog;

namespace iBurguer.Menu.Infrastructure.Logger;

[ExcludeFromCodeCoverage]
public static class LoggerHostApplicationExtensions
{
    public static IHostApplicationBuilder AddSerilog(this IHostApplicationBuilder builder)
    {
        var webApplicationBuilder = (WebApplicationBuilder)builder;

        webApplicationBuilder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(hostingContext.Configuration)
                .WriteTo.Console(new EcsTextFormatter(new()
                {
                    IncludeHost = false,
                    IncludeProcess = false,
                    IncludeUser = false
                }));
        });

        return webApplicationBuilder;
    }
}