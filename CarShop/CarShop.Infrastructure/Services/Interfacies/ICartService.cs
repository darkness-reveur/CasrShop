using CarShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCartAsync();

        Task<Cart> AddCartAsync(Cart newCart);

        Task<Cart> GetCartByIdAsync(int cartId);

        Task<bool> AddNewCarInCartAsync(int newCarId, int userId);

        Task<bool> DeleteCarOutCartAsync(int carId, int cartId);

        Task<bool> DeleteCartAsync(int basketId);
    }
}
