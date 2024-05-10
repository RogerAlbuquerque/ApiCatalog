using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Repositories;
                                                    // This is here down, is the new way to use 'base()'
public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    public IEnumerable<Product> GetProducts(ProductsParameters productsParams)
    {
        return GetAll()
            .OrderBy(p => p.Name)
            .Skip((productsParams.PageNumber - 1) * productsParams.PageSize)
            .Take(productsParams.PageSize).ToList();
    }

    public IEnumerable<Product> GetProductsByCategory(int id)
    {
        return GetAll().Where(c => c.CategoryId == id);
    }
}
