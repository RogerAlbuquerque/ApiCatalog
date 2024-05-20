using ApiCatalog.Context;
using ApiCatalog.Filters;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Repositories.Interfaces;
using ApiCatalog.DTOs;

namespace ApiCatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(IUnitOfWork uof, ILogger<CategoriesController> logger) : ControllerBase
{
    private readonly IUnitOfWork _uof = uof;
    public readonly ILogger<CategoriesController> _logger = logger;

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]

    public ActionResult<IEnumerable<CategoryDTO>> Get()
    {
        var categories = _uof.CategoryRepository.GetAll();
        var categoriesDto = new List<CategoryDTO>();
        foreach (var category in categories)
        {
            var categoryDto = new CategoryDTO()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
            };
            categoriesDto.Add(categoryDto);
        }
        return Ok(categoriesDto);
    }

   
    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<CategoryDTO> Get(int id)
    {
        var category = _uof.CategoryRepository.GetById(c => c.CategoryId == id);

        if (category == null)
        {
            return NotFound("Category not found");
        }

        var categoryDto = new CategoryDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            ImageUrl = category.ImageUrl,
        };

        return Ok(categoryDto);
    }

    [HttpPost]
    public ActionResult<CategoryDTO> Post(CategoryDTO categoryDto)
    {
        if (categoryDto == null) return BadRequest();


        var category = new Category()
        {
            CategoryId = categoryDto.CategoryId,
            Name = categoryDto.Name,
            ImageUrl = categoryDto.ImageUrl,
        };

        // the "Create" method, expect a "category" object, this is why 'category' was created, to put this data in this method
        var createdCategory = _uof.CategoryRepository.Create(category);
        _uof.Commit();

        //This convertion here is because The 'return' needed to be "CategoryDTO". The variable that comes from argument not be use, because in the
        //'create' method must have data manipulation, like transpose password in hash, so the password that comes from client, can not be showed here
        // using this convertion, the hash is what will be returned
        var newCategoryDto = new CategoryDTO()
        {
            CategoryId = createdCategory.CategoryId,
            Name = createdCategory.Name,
            ImageUrl = createdCategory.ImageUrl,
        };

        return new CreatedAtRouteResult("GetCategory", new { id = newCategoryDto.CategoryId }, newCategoryDto);
    }


    [HttpPut("{id:int}")]
    public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
    {
        if (id != categoryDto.CategoryId) return BadRequest();

        var category = new Category()
        {
            CategoryId = categoryDto.CategoryId,
            Name = categoryDto.Name,
            ImageUrl = categoryDto.ImageUrl,
        };

        _uof.CategoryRepository.Update(category);
        _uof.Commit();

        return Ok(categoryDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoryDTO> Delete(int id)
    {
        var category = _uof.CategoryRepository.GetById(c => c.CategoryId == id);
        if (category == null) return NotFound("Category Not Found");
        var excludedCategory = _uof.CategoryRepository.Delete(category);
        _uof.Commit();

        var categoryDto = new CategoryDTO()
        {
            CategoryId = excludedCategory.CategoryId,
            Name = excludedCategory.Name,
            ImageUrl = excludedCategory.ImageUrl,
        };
        return Ok(categoryDto);
    }
}
