using ApiCatalog.Context;
using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
private readonly AppDbContext _context = context;

public IEnumerable<Category> GetCategories()
{
    return _context.Categories.ToList();
}

public Category GetCategoryById(int id)
{
    return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
}

public Category CreateCategory(Category category)
{
    //if (category is null) 
    //    throw new ArgumentNullException(nameof(category));
    ArgumentNullException.ThrowIfNull(category);
    _context.Categories.Add(category);
    _context.SaveChanges();
    return category;
}


public Category UpdateCategory(Category category)
{
    //if (category is null)
    //    throw new ArgumentNullException(nameof(category));
    ArgumentNullException.ThrowIfNull(category);

    _context.Entry(category).State = EntityState.Modified;
    _context.SaveChanges();
    return category;

}

public Category DeleteCategory(int id)
{
    var category = _context.Categories.Find(id);

    if (category is null)
        //throw new ArgumentNullException(nameof(category));    // i dont know why but this causes an 'caution' message
        throw new ArgumentNullException(null, nameof(category));

    _context.Categories.Remove(category);
    _context.SaveChanges();
    return category;
}
}