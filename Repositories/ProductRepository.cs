using ApiCatalog.Context;
using ApiCatalog.Models;

namespace ApiCatalog.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public IQueryable<Product> GetProducts()
    {
        return _context.Products;
    }

    public Product GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(c => c.ProductId == id);
    }


    public Product CreatProduct(Product product)
    {
        throw new NotImplementedException();
    }


    public bool UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }


    public bool DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }



}
