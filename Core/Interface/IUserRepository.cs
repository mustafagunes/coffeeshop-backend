using System.Threading.Tasks;
using Core.Model;

namespace Core.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetWithEmailAsync(string email);
    }
}