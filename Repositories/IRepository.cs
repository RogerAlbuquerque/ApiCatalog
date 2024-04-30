using System.Linq.Expressions;

namespace ApiCatalog.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Expression<Func<T,bool>> predicate);  // It's called like this "GetById(c => c.ProductId == id)"
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
