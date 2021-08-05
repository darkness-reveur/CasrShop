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
    public class OrderService : IOrderService
    {
        private readonly CarShopContext _shopContext;

        private readonly ILogger<OrderService> _logger;

        public OrderService(
            CarShopContext shopContext,
            ILogger<OrderService> logger)
        {
            _logger = logger;
            _shopContext = shopContext;
        }

        public async Task<bool> DeleteOrderDyIdAsync(int orderId)
        {
            try
            {
                var exOrder = await _shopContext.Orders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(order => order.Id == orderId);

                _shopContext.Orders.Remove(exOrder);

                await _shopContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(DeleteOrderDyIdAsync),
                    $"Failed to delete Order with Id: {orderId} ",
                    ex);

                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                var orders =await _shopContext.Orders
                    .Include(order => order.User)
                    .Include(order => order.Cars)
                    .ThenInclude(cars => cars.CarModel)
                    .ThenInclude(model => model.CarBrand)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetAllOrdersAsync),
                    $"Cannot get datas about Orders from database ",
                    ex);

                return null;
            }
        }

        public async Task<IEnumerable<Order>> GerAllUserOrdersAsync(int userId)
        {
            if (userId == 0)
            {
                return null;
            }

            try
            {
                var userOrders = _shopContext.Orders
                    .Include(order => order.User)
                    .Include(order => order.Cars)
                        .ThenInclude(cars => cars.CarModel)
                            .ThenInclude(models => models.CarBrand)
                    .Where(order => order.UserId == userId)
                    .ToListAsync();

                return await userOrders;
            }
            catch(Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GerAllUserOrdersAsync),
                    $"Cannot get datas about Orders from database ",
                    ex);

                return null;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _shopContext.Orders
                    .Include(order => order.User)
                    .Include(order => order.Cars)
                    .ThenInclude(cars => cars.CarModel)
                    .ThenInclude(model => model.CarBrand)
                    .FirstOrDefaultAsync(order => order.Id == orderId);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetOrderByIdAsync),
                    $"Cannot get Order with Id: {orderId} !",
                    ex);

                return null;
            }
        }

        public async Task<Order> ApproveOrderAsync(int orderId)
        {
            if (orderId == 0)
            {
                return null;
            }
            try
            {
                var exOrder = await _shopContext.Orders
                    .FirstOrDefaultAsync(order => order.Id == orderId);

                if (exOrder.Cars is null)
                {
                    return null;
                }
                exOrder.OrderStatus = Order.OrderStatuses.Paid;
                                
                await _shopContext.SaveChangesAsync();

                return exOrder;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(ApproveOrderAsync),
                    $"Failed to update Order with Id: {orderId}",
                    ex);

                return null;
            }
        }

        public async Task<Order> CreateOrderAsync(int cartId)
        {
            if (cartId == 0)
            {
                return null;
            }
            try
            {
                Cart cart = await _shopContext.Carts
                    .Include(cart => cart.CartsCars)
                        .ThenInclude(cartCar => cartCar.Car)
                    .FirstOrDefaultAsync(cart => cart.Id == cartId);

                double totalPrice = 0;

                List<Car> cars = new List<Car>();

                foreach (var car in cart.CartsCars)
                {
                    cars.Add(car.Car);

                    totalPrice += car.Car.Price;
                }

                var order = new Order
                {
                    Date = DateTime.Now,
                    OrderStatus = Order.OrderStatuses.InProgress,
                    TotalAmount = totalPrice,
                    Cars = cars
                };

                await _shopContext.Orders.AddAsync(order);

                _shopContext.Cars.RemoveRange(cars);

                await _shopContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                User user =await _shopContext.Users
                    .FirstOrDefaultAsync(user => user.CartId == cartId);

                _logger.LogErrorByTemplate(
                  nameof(OrderService),
                  nameof(CreateOrderAsync),
                  $"Failed to add new order for user: {user.Name} with email: {user.Email}",
                  ex);
                return null;
            }
        }
    }
}
