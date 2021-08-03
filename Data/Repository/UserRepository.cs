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
        private CoffeeShopDbContext DbContext
        {
            get => _context as CoffeeShopDbContext;
        }
        
        public UserRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        public async Task<User> GetWithEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = from u in DbContext.Users select u;

            await user.SingleOrDefaultAsync(cancellationToken);
            
            user = user.Where (u => u.Email.ToLower().Contains(email));
            var result = user.ToArray().First();
            return result;
        }
    }
}