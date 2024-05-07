using ApiCatalog.Context;
using ApiCatalog.Repositories.Interfaces;

namespace ApiCatalog.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IProductRepository _productRepo ;
        private ICategoryRepository _categorytRepo ;
        public AppDbContext _context = context ;

        public IProductRepository ProductRepository 
        { 
            get
            {
                //return _productRepo = _productRepo ?? new ProductRepository(_context);    // Not using compound assignment but is the same code
                return _productRepo ??= new ProductRepository(_context);
                
                //#### The same code ####
                //if(_productRepo == null)
                //{
                //    _productRepo = new ProductRepository(_context);
                //}
                //else return _productRepo;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categorytRepo ??= new CategoryRepository(_context);
            }
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
