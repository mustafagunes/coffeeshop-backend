using System.Threading;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        // async method pass cancellationToken
        Task<User> GetWithEmailAsync(string email, CancellationToken cancellationToken);
        
        Task<User> Login(string email, string password, CancellationToken cancellationToken);
    }
}