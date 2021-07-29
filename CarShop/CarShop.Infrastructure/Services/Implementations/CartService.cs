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
                   $"Failed to add new user: {newCart.User.Name} with email: {newCart.User.Email} \"{newCart.UserId}\"",
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
                var order = await _shopContext.Carts
                    .Include(cart => cart.User)
                    .Include(cart => cart.Cars)
                        .ThenInclude(cars => cars.CarModel)
                            .ThenInclude(model => model.CarBrand)
                    .FirstOrDefaultAsync(cart => cart.Id == cartId);

                return order;
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

        public async Task<Cart> AddNewCarInCartAsync(int newCarId, int userId)
        {
            if (newCarId == 0 || userId == 0)
            {
                return null;
            }
            try
            {
                var helperUserWithTheCart = await _shopContext.Users
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .FirstOrDefaultAsync(user => user.Id == userId);

                var car = await _carService.GetCarByIdAsync(newCarId);

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

                    await _shopContext.SaveChangesAsync();
                }
                else
                {
                    helperUserWithTheCart.Cart.Cars.Add(car);

                    await _shopContext.SaveChangesAsync();
                }
                return helperUserWithTheCart.Cart;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                       nameof(CartService),
                       nameof(AddNewCarInCartAsync),
                       $"Failed to create cart for user (userId={userId})",
                       ex);
                return null;
            }
        }

        public async Task<bool> DeleteCartAsync(int basketId)
        {
            try
            {
                var exCart =await _shopContext.Carts
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

        public async Task<bool> DeleteCarOutCartAsync(int newCarId, int userId)
        {
            if (newCarId == 0 || userId == 0)
            {
                return false;
            }
            try
            {
                var helperUserWithTheCart = await _shopContext.Users
                    .Include(user => user.Cart)
                        .ThenInclude(cart => cart.Cars)
                    .FirstOrDefaultAsync(user => user.Id == userId);

                var carInCart = helperUserWithTheCart.Cart.Cars
                    .FirstOrDefault(car => car.Id == newCarId);

                if (carInCart is null)
                {
                    return false;
                }

                helperUserWithTheCart.Cart.Cars.Remove(carInCart);

                await _shopContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                       nameof(CartService),
                       nameof(AddNewCarInCartAsync),
                       $"Failed to delete car out cart for user (userId={userId})",
                       ex);
                return false;
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
