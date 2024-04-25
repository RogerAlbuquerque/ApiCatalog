using ApiCatalog.Models;

namespace ApiCatalog.Repositories
{
    public interface ICategoryRepository    // Since the interface is public, all its members are implicitly public as well.
    {
        IEnumerable<Category> GetCategories();        
        Category GetCategoryById(int id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        Category DeleteCategory(int id);
    }

}
