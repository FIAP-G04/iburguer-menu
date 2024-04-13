using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace iBurguer.Menu.Infrastructure.Swagger;

[ExcludeFromCodeCoverage]
public static class SwaggerHostApplicationExtensions
{
    private const string Title = "iBurguer Menu API";
    private const string Description = "API destinada ao subdomínio Gestão de Cardápio";
    private const string Version = "v1";
    
    public static IHostApplicationBuilder AddSwagger(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(Version, new OpenApiInfo
            {
                Title = Title, 
                Description = Description, 
                Version = Version
            });
            
            options.EnableAnnotations();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "iBurguer.Menu.API.xml"));
            options.DescribeAllParametersInCamelCase();
        });

        return builder;
    }
    
    public static void ConfigureSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Title} {Version}");
        });
    }
}