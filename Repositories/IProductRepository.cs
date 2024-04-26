using ApiCatalog.Models;

namespace ApiCatalog.Repositories
{   
    // This is not the more usable way to implement repository, but, i need learn
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts();
        Product GetProductById(int id);
        Product CreatProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int id);
    }
}
