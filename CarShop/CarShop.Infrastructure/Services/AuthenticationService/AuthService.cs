using CarShop.Businnes.Cryptographers;
using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Common.Models.AuthenticationModels;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly CarShopContext _carShopContext;

        private readonly ILogger<AuthService> _logger;

        private readonly IUserService _userService;

        public AuthService(
            CarShopContext carShopContext,
            IUserService userService,
            ILogger<AuthService> logger)
        {
            _carShopContext = carShopContext;
            _logger = logger;
            _userService = userService;
        }

        public async Task<bool> IsLoginFree(string login)
        {
            var acessData = await _carShopContext.AccessData
                .FirstOrDefaultAsync(data => data.Login == login);

            if (acessData == null)
            {
                return true;
            }

            return false;
        }

        public async Task<User> LogIn(string login, string password)
        {
            var salt = await GetUserSaltByLogin(login);

            var passwordSalt = Cryptographer.Encrypt(password, salt);

            var accessData = await _carShopContext.AccessData
                .FirstOrDefaultAsync(data => data.Login == login
                && data.PasswordSalt == passwordSalt);

            return accessData.User;
        }

        public async Task Register(string login, string password, User user)
        {
            var encryptedPassword = Cryptographer.Encrypt(password, out byte[] salt);

            var accessData = new AccessDataEntity
            {
                Login = login,
                PasswordSalt = encryptedPassword,
                UserId = user.Id,
                User = user,
                Salt = salt,
            };

            await _userService.AddAsync(user);

            await _carShopContext.AddAsync(accessData);
        }

        private async Task<byte[]> GetUserSaltByLogin(string login)
        {
            var accessData = await _carShopContext.AccessData
                .FirstOrDefaultAsync(accessDataEntity => accessDataEntity.Login == login);

            return accessData.Salt;
        }
    }
}
