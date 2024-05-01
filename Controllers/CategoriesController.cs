using ApiCatalog.Context;
using ApiCatalog.Filters;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger) : ControllerBase
{
    private readonly ICategoryRepository _repository = repository;
    public readonly ILogger<CategoriesController> _logger = logger;

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]

    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _repository.GetCategories();

        return Ok(categories);
    }

   
    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _repository.GetCategoryById(id);

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

        _repository.CreateCategory(category);

        return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId) return BadRequest();

        _repository.UpdateCategory(category);

        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> Delete(int id)
    {
        var category = _repository.GetCategoryById(id);
        if (category == null) return NotFound("Category Not Found");
        _repository.DeleteCategory(id);
        return Ok(category);
    }
}
