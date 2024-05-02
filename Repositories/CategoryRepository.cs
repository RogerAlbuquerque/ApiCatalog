using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Repositories;
                                                    // This is here down, is the new way to use 'base()'
public class CategoryRepository(AppDbContext context) : Repository<Category>(context),ICategoryRepository
{}