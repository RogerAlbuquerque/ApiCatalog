using ApiCatalog.Models;

namespace ApiCatalog.Repositories.Interfaces
{
    public interface ICategoryRepository :IRepository<Category>    // Since the interface is public, all its members are implicitly public as well.
    {
    
    }

}
