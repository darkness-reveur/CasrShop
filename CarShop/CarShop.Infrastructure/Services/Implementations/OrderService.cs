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
        public Cart AddCarToCard(int carId, int userId)
        {

            throw new NotImplementedException();
        } // not started

        public Order AddNewOrder(Order newOrder)
        {
            if (newOrder.Id != 0)
            {
                return null;
            }

            try
            {
                var simultaneousOrder = _shopContext.Orders
                    .FirstOrDefault(order => order.Date == newOrder.Date
                    && order.UserId == newOrder.UserId);

                var doubleOrder = _shopContext.Orders
                    .FirstOrDefault(order => order.UserId == newOrder.UserId
                    && order.OrderStatus == newOrder.OrderStatus
                    && order.OrderStatus == Order.OrderStatuses.Opend);

                if ((simultaneousOrder is null) && (doubleOrder is null))
                {
                    _shopContext.Orders
                        .Add(newOrder);

                    _shopContext.SaveChanges();
                }
                return newOrder;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(AddNewOrder),
                    $"Failed to add new Order of user: UserId={newOrder.UserId}, Date:{newOrder.Date}",
                    ex);

                return null;
            }
        }

        public bool DeleteOrderDyId(int orderId)
        {
            try
            {
                var exOrder = _shopContext.Orders
                    .AsNoTracking()
                    .FirstOrDefault(order => order.Id == orderId);

                _shopContext.Orders.Remove(exOrder);

                _shopContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(DeleteOrderDyId),
                    $"Failed to delete Order with Id: {orderId} ",
                    ex);

                return false;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                var orders = _shopContext.Orders
                    .Include(order => order.User)
                    .Include(order => order.Cars)
                    .ThenInclude(cars => cars.CarModel)
                    .ThenInclude(model => model.CarBrand);

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetAllOrders),
                    $"Cannot get datas about Orders from database ",
                    ex);

                return null;
            }
        }

        public Order GetOrderById(int orderId)
        {
            try
            {
                var order = _shopContext.Orders
                    .Include(order => order.User)
                    .Include(order => order.Cars)
                    .ThenInclude(cars => cars.CarModel)
                    .ThenInclude(model => model.CarBrand)
                    .FirstOrDefault(order => order.Id == orderId);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(OrderService),
                    nameof(GetOrderById),
                    $"Cannot get Order with Id: {orderId} !",
                    ex);

                return null;
            }
        }

        public bool DeleteCarOutCard(int carId, int userId)
        {
            return false;
        }

        public Order UpdateOrder(Order newOrder)
        {
            try
            {
                var exOrder = _shopContext.Orders
                    .FirstOrDefault(order => order.Id == newOrder.Id);

                if (!(exOrder is null))
                {
                    exOrder.OrderStatus = newOrder.OrderStatus;
                    exOrder.TotalAmount = newOrder.TotalAmount;
                    exOrder.Date = newOrder.Date;

                    _shopContext.SaveChanges();

                    return exOrder;
                }

                _shopContext.SaveChanges();

                return exOrder;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(UpdateOrder),
                    $"Failed to update Order with Id: {newOrder.Id}",
                    ex);

                return null;
            }
        }
    }
}
