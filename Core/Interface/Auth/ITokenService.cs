using System.Threading.Tasks;
using Core.Model.Auth;

namespace Core.Interface.Auth
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}