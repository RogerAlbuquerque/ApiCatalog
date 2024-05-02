using ApiCatalog.Models;

namespace ApiCatalog.Repositories.Interfaces
{
    // This is not the more usable way to implement repository, but, i need learn
    public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetProductsByCategory(int id);
    }
}
