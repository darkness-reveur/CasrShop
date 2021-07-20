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
    public class CartService : ICartService
    {
        private readonly CarShopContext _shopContext;

        private readonly ICarService _carService;

        private readonly ILogger<CartService> _logger;

        public CartService(
            CarShopContext shopContext,
            ILogger<CartService> logger,
            ICarService carService)
        {
            _carService = carService;
            _logger = logger;
            _shopContext = shopContext;
        }

        public Cart AddCart(Cart newCart)
        {
            if (newCart.Id != 0)
            {
                return null;
            }

            try
            {
                _shopContext.Carts.Add(newCart);

                return newCart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(CartService),
                   nameof(AddCart),
                   $"Failed to add new user: {newCart.User.Name} with email: {newCart.User.Email} \"{newCart.UserId}\"",
                   ex);
                return null;
            }
        }

        public Cart GetCartById(int cartId)
        {
            if (cartId == 0)
            {
                return null;
            }

            try
            {
                var order = _shopContext.Carts
                    .Include(cart => cart.User)
                    .Include(cart => cart.Cars)
                        .ThenInclude(cars => cars.CarModel)
                            .ThenInclude(model => model.CarBrand)
                    .FirstOrDefault(cart => cart.Id == cartId);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CartService),
                    nameof(GetCartById),
                    $"Cannot get Cart with Id: {cartId} !",
                    ex);

                return null;
            }
        }

        public Cart AddNewCarInCart(int newCarId, int userId)
        {
            if (newCarId == 0 || userId == 0)
            {
                return null;
            }
            try
            {
                var helperUserWithTheCart = _shopContext.Users
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .FirstOrDefault(user => user.Id == userId);

                var car = _carService.GetCarById(newCarId);

                if (car is null)
                {
                    return null;
                }

                if (helperUserWithTheCart.Cart is null)
                {
                    helperUserWithTheCart.Cart = new Cart()
                    {
                        Cars = new List<Car>()
                    };
                    helperUserWithTheCart.Cart.Cars.Add(car);

                    _shopContext.SaveChanges();
                }
                else
                {
                    helperUserWithTheCart.Cart.Cars.Add(car);

                    _shopContext.SaveChanges();
                }
                return helperUserWithTheCart.Cart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                       nameof(CartService),
                       nameof(AddNewCarInCart),
                       $"Failed to create cart for user (userId={userId})",
                       ex);
                return null;
            }
        }

        public bool DeleteCart(int basketId)
        {
            try
            {
                var exCart = _shopContext.Carts
                    .AsNoTracking()
                    .FirstOrDefault(basket => basket.Id == basketId);

                _shopContext.Carts.Remove(exCart);

                _shopContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CartService),
                    nameof(DeleteCart),
                    $"Failed to delete basket with Id: {basketId} ",
                    ex);

                return false;
            }
        }

        public bool DeleteCarOutCart(int newCarId, int userId)
        {
            if (newCarId == 0 || userId == 0)
            {
                return false;
            }
            try
            {
                var helperUserWithTheCart = _shopContext.Users
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .FirstOrDefault(user => user.Id == userId);

                var carInCart = helperUserWithTheCart.Cart.Cars
                    .FirstOrDefault(car => car.Id == newCarId);

                if (carInCart is null)
                {
                    return false;
                }

                helperUserWithTheCart.Cart.Cars.Remove(carInCart);

                _shopContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                       nameof(CartService),
                       nameof(AddNewCarInCart),
                       $"Failed to delete car out cart for user (userId={userId})",
                       ex);
                return false;
            }
        }

        public IEnumerable<Cart> GetAllCart()
        {
            try
            {
                var basket = _shopContext.Carts
                    .ToList();

                return basket;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CartService),
                    nameof(GetAllCart),
                    $"Cannot get datas about Carts from database !",
                    ex);

                return null;
            }
        }

    }
}
