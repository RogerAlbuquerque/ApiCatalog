using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Repositories;
                                                    // This is here down, is the new way to use 'base()'
public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    public IEnumerable<Product> GetProductsByCategory(int id)
    {
        return GetAll().Where(c => c.CategoryId == id);
    }
}
