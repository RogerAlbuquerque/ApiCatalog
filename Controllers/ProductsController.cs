using ApiCatalog.Context;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Models;

namespace ApiCatalog.Controllers;

//[Route("[controller]/{action}")]
[Route("[controller]")]
[ApiController]
public class ProductController(AppDbContext context) : ControllerBase
{
    public readonly AppDbContext _context = context;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _context.Products.ToList();
        if(products is null)
        {
            return NotFound("Products not found");
        }
        return products;
    }

    [HttpGet("{id:int}", Name ="GetProduct")]
    public ActionResult<Product> GetById(int id)
    {
        var products = _context.Products.FirstOrDefault(p => p.ProductId == id);
        if (products is null)
        {
            return NotFound("Products not found");
        }
        return products;
    }

    [HttpPost]
    public ActionResult Post(Product product)   
    {
        if (product is null)
        {
            return BadRequest("Product not found");
        }
        _context.Products?.Add(product);        
        _context.SaveChanges();         
        return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);

    }


}