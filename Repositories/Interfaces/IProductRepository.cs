using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repositories.Interfaces
{
    // This is not the more usable way to implement repository, but, i need learn
    public interface IProductRepository:IRepository<Product>
    {
       PagedList<Product> GetProducts(ProductsParameters productsParams);
       IEnumerable<Product> GetProductsByCategory(int id);
    }
}
