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
        List<User> GetAll();

        User Add(User user);

        User GetUserById(int userId);

        User Update(User user);
    }
}
