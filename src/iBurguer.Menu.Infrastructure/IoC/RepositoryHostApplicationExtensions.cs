using System.Diagnostics.CodeAnalysis;
using iBurguer.Menu.Core.Domain;
using iBurguer.Menu.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Menu.Infrastructure.IoC;

[ExcludeFromCodeCoverage]
public static class RepositoryHostApplicationExtensions
{
    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMenuRepository, MenuRepository>();

        return builder;
    }
}