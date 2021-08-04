using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private CoffeeShopDbContext _dbContext
        {
            get => _context as CoffeeShopDbContext;
        }
        
        public UserRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        public async Task<User> GetWithEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user;
        }

        public async Task<User> Login(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password, cancellationToken);
            return user;
        }
    }
}