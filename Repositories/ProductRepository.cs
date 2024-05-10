using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Repositories;
                                                    // This is here down, is the new way to use 'base()'
public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
{
    public PagedList<Product> GetProducts(ProductsParameters productsParams)
    {
        var products =  GetAll().OrderBy(p => p.ProductId).AsQueryable();
        return PagedList<Product>.ToPagedList(products, productsParams.PageNumber, productsParams.PageSize);
       
        //return GetAll()
        //    .OrderBy(p => p.Name)
        //    .Skip((productsParams.PageNumber - 1) * productsParams.PageSize)
        //    .Take(productsParams.PageSize).ToList();
    }

    public IEnumerable<Product> GetProductsByCategory(int id)
    {
        return GetAll().Where(c => c.CategoryId == id);
    }
}
