using System.Diagnostics.CodeAnalysis;
using iBurguer.Menu.Core.UseCases.AddMenuItem;
using iBurguer.Menu.Core.UseCases.Categories;
using iBurguer.Menu.Core.UseCases.ChangeMenuItem;
using iBurguer.Menu.Core.UseCases.DisableMenuItem;
using iBurguer.Menu.Core.UseCases.EnableMenuItem;
using iBurguer.Menu.Core.UseCases.GetMenuItem;
using iBurguer.Menu.Core.UseCases.MenuItemsByCategory;
using iBurguer.Menu.Core.UseCases.RemoveMenuItem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iBurguer.Menu.Infrastructure.IoC;

[ExcludeFromCodeCoverage]
public static class UseCaseHostApplicationExtensions
{
    public static IHostApplicationBuilder AddUseCases(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAddMenuItemToMenuUseCase, AddItemToMenuUseCase>()
                        .AddScoped<IChangeMenuItemUseCase, ChangeMenuItemUseCase>()
                        .AddScoped<IDisableMenuItemUseCase, DisableMenuItemUseCase>()
                        .AddScoped<IEnableMenuItemUseCase, EnableMenuItemUseCase>()
                        .AddScoped<IRemoveItemFromMenuUseCase, RemoveItemFromMenuUseCase>()
                        .AddScoped<IGetCategorizedMenuItemsUseCase, GetCategorizedMenuItemsUseCase>()
                        .AddScoped<IGetMenuItemUseCase, GetMenuItemUseCase>()
                        .AddScoped<IGetCategoriesUseCase, GetCategoriesUseCase>();

        return builder;
    }
}