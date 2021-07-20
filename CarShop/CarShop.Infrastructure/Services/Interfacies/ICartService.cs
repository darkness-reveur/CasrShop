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
        IEnumerable<Cart> GetAllCart();

        Cart AddCart(Cart newCart);

        public Cart GetCartById(int cartId);

        Cart AddNewCarInCart(int newCarId, int userId);

        bool DeleteCarOutCart(int carId, int cartId);

        bool DeleteCart(int basketId);
    }
}
