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

        public async Task<Cart> AddCartAsync(Cart newCart)
        {
            if (newCart.Id != 0)
            {
                return null;
            }

            try
            {
                await _shopContext.Carts.AddAsync(newCart);

                return newCart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(CartService),
                   nameof(AddCartAsync),
                   $"Failed to add new user",
                   ex);
                return null;
            }
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            if (cartId == 0)
            {
                return null;
            }

            try
            {
                var cart = await _shopContext.Carts
                    .Include(cart => cart.CartsCars)
                        .ThenInclude(cartsCars => cartsCars.Car)
                            .ThenInclude(cars => cars.CarModel)
                                .ThenInclude(model => model.CarBrand)
                    .FirstOrDefaultAsync(cart => cart.Id == cartId);

                return cart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CartService),
                    nameof(GetCartByIdAsync),
                    $"Cannot get Cart with Id: {cartId} !",
                    ex);

                return null;
            }
        }

        public async Task<bool> AddNewCarInCartAsync(int newCarId, int userId)
        {
            if (newCarId == 0 || userId == 0)
            {
                return false;
            }
            try
            {
                var helperUserWithTheCart = await _shopContext.Users
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.CartsCars)
                            .ThenInclude(cartCar => cartCar.Car)
                    .FirstOrDefaultAsync(user => user.Id == userId);

                var car = await _carService.GetCarByIdAsync(newCarId);

                if (car is null
                    || helperUserWithTheCart is null)
                {
                    return false;
                }

                if (helperUserWithTheCart.Cart is null)
                {
                    helperUserWithTheCart.Cart = new Cart();
                }

                CartCar cartCar = new CartCar
                {
                    Car = car,
                    Cart = helperUserWithTheCart.Cart
                };

                helperUserWithTheCart.Cart.CartsCars.Add(cartCar);


                await _shopContext.SaveChangesAsync();


                //if (helperUserWithTheCart.Cart is null)
                //{
                //    helperUserWithTheCart.Cart = new Cart()
                //    {
                //        Cars = new List<Car>()
                //    };
                //    helperUserWithTheCart.Cart.Cars.Add(car);

                //    await _shopContext.SaveChangesAsync();
                //}
                //else
                //{
                //    helperUserWithTheCart.Cart.Cars.Add(car);

                //    await _shopContext.SaveChangesAsync();
                //}
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                       nameof(CartService),
                       nameof(AddNewCarInCartAsync),
                       $"Failed to create cart for user (userId={userId})",
                       ex);
                return false;
            }
        }

        public async Task<bool> DeleteCartAsync(int basketId)
        {
            try
            {
                var exCart = await _shopContext.Carts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(basket => basket.Id == basketId);

                _shopContext.Carts.Remove(exCart);

                await _shopContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CartService),
                    nameof(DeleteCartAsync),
                    $"Failed to delete basket with Id: {basketId} ",
                    ex);

                return false;
            }
        }



        public async Task<Cart> DeleteCarOutCartAsync(int newCarId, int userId)
        {
            if (newCarId == 0 || userId == 0)
            {
                return null;
            }
            try
            {
                var helperUserWithCart = await _shopContext.Users
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.CartsCars)
                            .ThenInclude(cartCar => cartCar.Car)
                    .FirstOrDefaultAsync(user => user.Id == userId);

                Car carInCart = null;
                foreach (CartCar cart in helperUserWithCart.Cart.CartsCars)
                {
                    carInCart = cart.Car;
                }

                if (carInCart is null)
                {
                    return null;
                }
                // нужно сделать в юзер контроллере удаление машины из карзины если этот метод правильный
                CartCar cartCar = helperUserWithCart.Cart.CartsCars.FirstOrDefault(cartcar => cartcar.Car == carInCart);

                helperUserWithCart.Cart.CartsCars.Remove(cartCar);

                await _shopContext.SaveChangesAsync();

                return helperUserWithCart.Cart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                       nameof(CartService),
                       nameof(AddNewCarInCartAsync),
                       $"Failed to delete car out cart for user (userId={userId})",
                       ex);
                return null;
            }
        }

        public async Task<IEnumerable<Cart>> GetAllCartAsync()
        {
            try
            {
                var basket = await _shopContext.Carts
                    .ToListAsync();

                return basket;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CartService),
                    nameof(GetAllCartAsync),
                    $"Cannot get datas about Carts from database !",
                    ex);

                return null;
            }
        }

    }
}
