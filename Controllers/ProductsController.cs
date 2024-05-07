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
public class ProductController(IUnitOfWork uof , IConfiguration configuration) : ControllerBase
{
    private readonly IUnitOfWork _uof = uof;
    private readonly IConfiguration _configurations = configuration;



    [HttpGet("ReadConfigFile")]
    public string GetValues()
    {
        var Value1 = _configurations["key1"];

        //if (string.IsNullOrEmpty(Value1))
        //    throw new Exception("Null Value");

        return Value1;
    }

    [HttpGet("products/{id}")]
    public ActionResult<IEnumerable<Product>> GetProductsByCategory(int id)
    { 
        var products = _uof.ProductRepository.GetProductsByCategory(id);
        if (products == null)
            return NotFound();

        return Ok(products);
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
        var products = _uof.ProductRepository.GetAll();
        if (products is null)
        {
            return NotFound("Products not found");
        }
        return Ok(products);
    }

    //[HttpGet("first", Name ="FirstProduct")]
    //public async Task<ActionResult<IEnumerable<Product>>> GetFirst()
    //{
    //    var products = await _repository.GetProducts().AsNoTracking().ToListAsync();
    //    if (products is null)
    //    {
    //        return NotFound("Products not found");
    //    }
    //    return products;
    //}

    [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
    public ActionResult<Product> GetProductById([BindRequired] int id)
    {
        var product = _uof.ProductRepository.GetById(p => p.ProductId == id);
        if (product is null)
        {
            return NotFound("Products not found");
        }
        return Ok(product);
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        if (product is null)
        {
            return BadRequest("Product not found");
        }
        _uof.ProductRepository.Create(product);
        _uof.Commit();
        return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, product);

    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }
        var ProductUpdated = _uof.ProductRepository.Update(product);
        _uof.Commit();

        return Ok(ProductUpdated);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) 
    {
        var product = _uof.ProductRepository.GetById(c => c.ProductId == id);
        if (product == null) return NotFound("Category Not Found");

        _uof.ProductRepository.Delete(product);
        _uof.Commit();
        return Ok(product);



    }
}