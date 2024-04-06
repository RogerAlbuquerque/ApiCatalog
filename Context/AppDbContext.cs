using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    //public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
    //{ }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}


