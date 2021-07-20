using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CarShopContext _carShopContext;

        private readonly ILogger<UserService> _logger;

        public UserService(
            CarShopContext carShopContext,
            ILogger<UserService> logger)
        {
            _carShopContext = carShopContext;
            _logger = logger;
        }

        public User Add(User newUser)   
        {
            if (newUser is null)
            {
                return null;
            }

            try
            {
                var exUser = _carShopContext.Users
                    .FirstOrDefault(user => user.MobilePhoneNumber == newUser.MobilePhoneNumber
                    || user.Email == newUser.Email);
                
                if (!(exUser is null))
                {
                    _carShopContext.Users.Add(newUser);

                    _carShopContext.SaveChanges();
                }

                return newUser;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(UserService),
                   nameof(_carShopContext),
                   $"Failed to add user with email={newUser.Email}",
                   ex);

                return null;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                var users = _carShopContext.Users
                    .Include(user => user.Order)
                        .ThenInclude(order => order.Cars)
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .Include(user => user.Car)
                    .ToList();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(GetAll),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                var user = _carShopContext.Users
                    .Include(user => user.Order)
                        .ThenInclude(order => order.Cars)
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .Include(user => user.Car)
                    .FirstOrDefault();


                return user;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(GetUserById),
                    $"Cannot get user with id={userId}",
                    ex);

                return null;
            }
        }

        public User Update(User newUser)
        {
            try
            {
                var exUser = _carShopContext.Users
                    .FirstOrDefault(user => user.Id == newUser.Id);

                if (!(exUser is null))
                {
                    exUser.MobilePhoneNumber = newUser.MobilePhoneNumber;
                    exUser.Name = newUser.Name;
                    exUser.Email = newUser.Email;
                    exUser.Age = newUser.Age;
                    exUser.Role = newUser.Role;

                    _carShopContext.SaveChanges();
                }

                return exUser;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                  nameof(UserService),
                  nameof(Update),
                  $"Failed to update user with email={newUser.Email}",
                  ex);

                return null;
            }
        }
    }
}
