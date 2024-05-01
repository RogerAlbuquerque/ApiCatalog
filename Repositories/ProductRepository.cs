using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Repositories.Interfaces;

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
        //if (product is null)
        //    throw new ArgumentNullException(nameof(product));
        //Instead all this, do this:
        ArgumentNullException.ThrowIfNull(product);

        _context.Products.Add(product);
        _context.SaveChanges();

        return product;
    }


    public bool UpdateProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (_context.Products.Any(p => p.ProductId == product.ProductId))
        { 
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }
        return false;
    }


    public bool DeleteProduct(int id)
    {
        var product = _context.Products.Find();

        if (product is not null)
        { 
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        return false;
    }



}
