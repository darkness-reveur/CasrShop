using CarShop.Common.Models;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.AuthenticationService
{
    public interface IAuthService
    {
        void Register(
           string login,
           string password,
           User user);

        bool IsLoginFree(string login);

        User LogIn(
            string login,
            string password);
    }
}
