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

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var products = _context.Products.FirstOrDefault(p => p.ProductId == id);
        if (products is null)
        {
            return NotFound("Products not found");
        }
        return products;
    }

}