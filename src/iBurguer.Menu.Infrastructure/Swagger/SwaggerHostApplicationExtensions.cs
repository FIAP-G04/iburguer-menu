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
    private const string Description = "The Digital Menu Management API provides a comprehensive solution for managing the digital menu of iBurguer. This RESTful API empowers restaurant owners and administrators to effortlessly update, organize, and customize their menu offerings in real-time, ensuring an engaging and dynamic experience for customers.";
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