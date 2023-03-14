using System.Threading.Tasks;
using Data.Context;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        // IProductRepository Products { get; }
        //
        // ICategoryRepository Categories { get; }
        
        Task CommitAsync();

        void Commit();
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        // Definitions
        private readonly CoffeeShopDbContext _context;
        
        // private ProductRepository _productRepository;
        // private CategoryRepository _categoryRepository;
        // public IProductRepository Products =>
        //     _productRepository ??= new ProductRepository(_context);
        // public ICategoryRepository Categories =>
        //     _categoryRepository ??= new CategoryRepository(_context);
        
        public UnitOfWork(CoffeeShopDbContext context)
        {
            _context = context;
        }
        
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}