using CarShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();

        Task<User> AddAsync(User user);

        Task<User> GetUserByIdAsync(int userId);

        Task<User> UpdateAsync(User user);
    }
}
