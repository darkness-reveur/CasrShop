using CarShop.Common.Models;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task Register(
           string login,
           string password,
           User user);

        Task<bool> IsLoginFree(string login);

        Task<User> LogIn(
            string login,
            string password);
    }
}
