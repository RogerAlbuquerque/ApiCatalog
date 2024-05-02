using ApiCatalog.Context;
using ApiCatalog.Filters;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(IRepository<Category> repository, ILogger<CategoriesController> logger) : ControllerBase
{
    private readonly IRepository<Category> _repository = repository;
    public readonly ILogger<CategoriesController> _logger = logger;

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]

    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _repository.GetAll();

        return Ok(categories);
    }

   
    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _repository.GetById(c => c.CategoryId == id);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        if (category == null) return BadRequest();

        _repository.Create(category);

        return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId) return BadRequest();

        _repository.Update(category);

        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> Delete(int id)
    {
        var category = _repository.GetById(c => c.CategoryId == id);
        if (category == null) return NotFound("Category Not Found");
        _repository.Delete(category);
        return Ok(category);
    }
}
