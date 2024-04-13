using iBurguer.Menu.Core.Domain;

namespace iBurguer.Menu.Core.UseCases.Categories;

public interface IGetCategoriesUseCase
{
    IEnumerable<string> GetCategories();
}

public class GetCategoriesUseCase : IGetCategoriesUseCase
{
    public IEnumerable<string> GetCategories()
    {
        return Category.ToList();
    }
}