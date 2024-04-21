using ApiCatalog.Context;
using ApiCatalog.Filters;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(AppDbContext context) : ControllerBase
{
    public readonly AppDbContext _context = context;

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]

    public ActionResult<IEnumerable<Category>> Get()
    {
        return _context.Categories.ToList();
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoryWProduct()
    { 
        return _context.Categories.Include(p => p.Products).ToList();
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        return Ok();
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        if (category == null) return BadRequest();

        _context.Categories.Add(category);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
    }


    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId) return BadRequest();

        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
        if (category == null) return NotFound("Category Not Found");
        _context.Categories.Remove(category);
        _context.SaveChanges();
        return Ok(category);
    }
}
