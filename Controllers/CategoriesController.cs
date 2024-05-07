using ApiCatalog.Context;
using ApiCatalog.Filters;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(IUnitOfWork uof, ILogger<CategoriesController> logger) : ControllerBase
{
    private readonly IUnitOfWork _uof = uof;
    public readonly ILogger<CategoriesController> _logger = logger;

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]

    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _uof.CategoryRepository.GetAll();

        return Ok(categories);
    }

   
    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _uof.CategoryRepository.GetById(c => c.CategoryId == id);

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

        _uof.CategoryRepository.Create(category);
        _uof.Commit();

        return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId) return BadRequest();

        _uof.CategoryRepository.Update(category);
        _uof.Commit();

        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> Delete(int id)
    {
        var category = _uof.CategoryRepository.GetById(c => c.CategoryId == id);
        if (category == null) return NotFound("Category Not Found");
        _uof.CategoryRepository.Delete(category);
        _uof.Commit();
        return Ok(category);
    }
}
