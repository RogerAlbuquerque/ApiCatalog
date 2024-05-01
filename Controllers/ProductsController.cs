using ApiCatalog.Context;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ApiCatalog.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Controllers;

//[Route("[controller]/{action}")]
[Route("[controller]")]
[ApiController]
public class ProductController(IProductRepository repository, IConfiguration configuration) : ControllerBase
{
    private readonly IProductRepository _repository = repository;
    private readonly IConfiguration _configurations = configuration;



    [HttpGet("ReadConfigFile")]
    public string GetValues()
    {
        var Value1 = _configurations["key1"];

        //if (string.IsNullOrEmpty(Value1))
        //    throw new Exception("Null Value");

        return Value1;
    }


    // HOW IT WAS USED 'FROMSERVICES' BEFORE .NET 7
    //[HttpGet("fromservice/{name}")]
    //public ActionResult<string> GetGreetingsFromServices([FromServices] IMyService myservice, string name)
    //{
    //    return myservice.Greeting(name);
    //}


    // AFTER .NET7
    [HttpGet("fromservice/{name}")]
    public ActionResult<string> GetGreetingsFromServices(IMyService myservice, string name)
    {
        return myservice.Greeting(name);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _repository.GetProducts().ToList();
        if (products is null)
        {
            return NotFound("Products not found");
        }
        return products;
    }

    [HttpGet("first", Name ="FirstProduct")]
    public async Task<ActionResult<IEnumerable<Product>>> GetFirst()
    {
        var products = await _repository.GetProducts().AsNoTracking().ToListAsync();
        if (products is null)
        {
            return NotFound("Products not found");
        }
        return products;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetById([BindRequired] int id)
    {
        var products = await _repository.GetProducts().FirstOrDefaultAsync(p => p.ProductId == id);
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
        _repository.CreatProduct(product);
        return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);

    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }
        bool updated = _repository.UpdateProduct(product);

        if (updated)
            return Ok(product);
        else
            return StatusCode(500, $"Error to update product with id = {id}");
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) 
    {
        bool product = _repository.DeleteProduct(id);
        if (product)
            return Ok($"Product with id = {id} was excluded");
        else
            return StatusCode(500, $"Error to delete product with id = {id}");


    }
}