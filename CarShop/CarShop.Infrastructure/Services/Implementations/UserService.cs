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

        public async Task<User> AddAsync(User newUser)   
        {
            if (newUser is null)
            {
                return null;
            }

            try
            {
                var exUser = await _carShopContext.Users
                    .FirstOrDefaultAsync(user => user.MobilePhoneNumber == newUser.MobilePhoneNumber
                    || user.Email == newUser.Email);
                
                if (!(exUser is null))
                {
                    await _carShopContext.Users.AddAsync(newUser);

                    await _carShopContext.SaveChangesAsync();
                }

                return newUser;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(UserService),
                   nameof(AddAsync),
                   $"Failed to add user with email={newUser.Email}",
                   ex);

                return null;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                var users = await _carShopContext.Users
                    .Include(user => user.Orders)
                        .ThenInclude(order => order.Cars)
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .Include(user => user.Car)
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(GetAllAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _carShopContext.Users
                    .Include(user => user.Orders)
                        .ThenInclude(order => order.Cars)
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .Include(user => user.Car)
                    .FirstOrDefaultAsync();


                return user;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(UserService),
                    nameof(GetUserByIdAsync),
                    $"Cannot get user with id={userId}",
                    ex);

                return null;
            }
        }

        public async Task<User> UpdateAsync(User newUser)
        {
            try
            {
                var exUser = await _carShopContext.Users
                    .FirstOrDefaultAsync(user => user.Id == newUser.Id);

                if (!(exUser is null))
                {
                    exUser.MobilePhoneNumber = newUser.MobilePhoneNumber;
                    exUser.Name = newUser.Name;
                    exUser.Email = newUser.Email;
                    exUser.Age = newUser.Age;
                    exUser.Role = newUser.Role;

                    await _carShopContext.SaveChangesAsync();
                }

                return exUser;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                  nameof(UserService),
                  nameof(UpdateAsync),
                  $"Failed to update user with email={newUser.Email}",
                  ex);

                return null;
            }
        }
    }
}
