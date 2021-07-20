using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Common.Models.AuthenticationModels;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
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

        public bool IsLoginFree(string login)
        {
            string hashedLogin = GetHashedValue(login);

            try
            {
                var users = _carShopContext.AccessData
                    .ToList();

                var accessData = users.FirstOrDefault(data => AreHashStringEquals(
                    data.Login,
                    hashedLogin));

                if (accessData == null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                      nameof(AuthService),
                      nameof(IsLoginFree),
                      $"Failed to get data from database",
                      ex);
            }

            return false;
        }

        public User LogIn(string login, string password)
        {
            string hashedLogin = GetHashedValue(login);

            var accessData = _carShopContext.AccessData
                .AsEnumerable()
                .FirstOrDefault(data => AreHashStringEquals(data.Login, hashedLogin));

            if (accessData != null)
            {
                string hashedPassword = GetHashedValue(password);

                if (AreHashStringEquals(hashedPassword, accessData.Password))
                {
                    return  _userService.GetUserById(accessData.UserId);
                }
            }

            return null;
        }

        public void Register(string login, string password, User user)
        {
            string hashedLogin = GetHashedValue(login);

            string hashedPassword = GetHashedValue(password);
           
            try
            {
                var accessData = new AccessDataEntity
                {
                    Login = hashedLogin,
                    Password = hashedPassword,
                    UserId = user.Id,
                    User = user
                };

                _carShopContext.AccessData
                    .Add(accessData);

                _carShopContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(AuthService),
                   nameof(Register),
                   $"Failed to add data to database",
                   ex);
            }
        }

        private string GetHashedValue(string sourceString)
        {
            byte[] sourceStringByteArray;

            byte[] hashedByteArray;

            sourceStringByteArray = ASCIIEncoding.ASCII.GetBytes(sourceString);

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            hashedByteArray = md5Hasher.ComputeHash(sourceStringByteArray);

            return ByteArrayToString(hashedByteArray);
        }

        private bool AreHashStringEquals(string firstValue, string secondValue)
        {
            bool areTheyEqual = false;

            if (firstValue.Length == firstValue.Length)
            {
                int i = 0;

                while ((i < firstValue.Length)
                    && (firstValue[i] == secondValue[i]))
                {
                    i += 1;
                }

                if (i == firstValue.Length)
                {
                    areTheyEqual = true;
                }
            }

            return areTheyEqual;
        }

        private string ByteArrayToString(byte[] inputValue)
        {
            StringBuilder result = new StringBuilder(inputValue.Length);

            for (int i = 0; i < inputValue.Length - 1; i++)
            {
                result.Append(inputValue[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
