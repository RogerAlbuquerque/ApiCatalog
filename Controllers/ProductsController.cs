using ApiCatalog.Context;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ApiCatalog.Services;
using ApiCatalog.Repositories.Interfaces;
using ApiCatalog.Pagination;
using ApiCatalog.DTOs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;

namespace ApiCatalog.Controllers;

//[Route("[controller]/{action}")]
[Route("[controller]")]
[ApiController]
public class ProductController(IUnitOfWork uof , IConfiguration configuration, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _uof = uof;
    private readonly IConfiguration _configurations = configuration;
    private readonly IMapper _mapper = mapper;



    [HttpGet("ReadConfigFile")]
    public string GetValues()
    {
        var Value1 = _configurations["key1"];

        //if (string.IsNullOrEmpty(Value1))
        //    throw new Exception("Null Value");

        return Value1;
    }










    //[HttpGet(Name = "Get1")]
    //public ActionResult<IEnumerable<ProductDTO>> Get1([FromQuery] ProductsParameters productsParameters)
    //{
    //    var products = _uof.ProductRepository.GetProducts(productsParameters);

    //    //var metadata = new
    //    //{
    //    //    products.TotalCount,
    //    //    products.PageSize,
    //    //    products.CurrentPage,
    //    //    products.TotalPages,
    //    //    products.HasNext,
    //    //    products.HasPrevious

    //    //};

    //    //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

    //    return Ok(products);
    //}












    [HttpGet("products/{id}")]
    public ActionResult<IEnumerable<ProductDTO>> GetProductsByCategory(int id)
    { 
        var products = _uof.ProductRepository.GetProductsByCategory(id);
        if (products == null)
            return NotFound();

      //var destinyResu = _mapper.Map<DestinyRsult>(origin); 
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

        return Ok(productsDto);
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










    [HttpGet(Name = "Get2")]
    public ActionResult<IEnumerable<ProductDTO>> Get()
    {
        var products = _uof.ProductRepository.GetAll();
        if (products is null)
        {
            return NotFound("Products not found");
        }
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
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

        var productDto = _mapper.Map<ProductDTO>(product);
        return Ok(productDto);
    }










    [HttpPost]
    public ActionResult<ProductDTO> Post(ProductDTO productDto)
    {
        if (productDto is null)
        {
            return BadRequest("Product not found");
        }

        var products = _mapper.Map<Product>(productDto);
        var updatedProduct = _uof.ProductRepository.Create(products);
        _uof.Commit();

        var updatedProductDto = _mapper.Map<ProductDTO>(updatedProduct);
        return new CreatedAtRouteResult("GetProduct", new { id = updatedProductDto.ProductId }, updatedProductDto);

    }












    [HttpPut("{id:int}")]
    public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
    {
        if (id != productDto.ProductId)
        {
            return BadRequest();
        }

        var product = _mapper.Map<Product>(productDto);

        var ProductUpdated = _uof.ProductRepository.Update(product);
        _uof.Commit();

        var ProductUpdatedDto = _mapper.Map<ProductDTO>(ProductUpdated);
        return Ok(ProductUpdatedDto);
    }










    [HttpDelete("{id:int}")]
    public ActionResult<ProductDTO> Delete(int id) 
    {
        var product = _uof.ProductRepository.GetById(c => c.ProductId == id);
        if (product == null) return NotFound("Category Not Found");

        var deletedProduct = _uof.ProductRepository.Delete(product);
        _uof.Commit();

        var deletedProductDto = _mapper.Map<ProductDTO>(deletedProduct);
        return Ok(deletedProductDto);



    }
}