using ApiCatalog.Context;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalog.Controllers;

[Route("[controller]/{action}")]
[ApiController]
public class ProductController(AppDbContext context) : ControllerBase
{
    public readonly AppDbContext _context = context;

}