using ApiCatalog.Context;
using ApiCatalog.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ApiCatalog.Repositories
{
    public class Repository<T>(AppDbContext context):IRepository<T> where T : class  // 'T' need to be a classe whit this last command
    {
        protected readonly AppDbContext _context = context;
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
            //return [.. _context.Set<T>()]; 
            //Ao utilizar o spread operator, você está dizendo explicitamente que deseja expandir todos os elementos do conjunto
            //retornado por _context.Set<T>() em um novo IEnumerable<T>, evitando a criação intermediária de uma lista.
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
           return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Update(T entity)
        {
            // _context.Entry(entity).State = EntityState.Modified; // A de baixo faz a mesma coisa que essa, com algumas diferenças. Esse cria uma query para um único campo, o outro é mais para atualizar tudo.
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entity;
        }


    }
}
