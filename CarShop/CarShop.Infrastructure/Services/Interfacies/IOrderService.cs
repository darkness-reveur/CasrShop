using CarShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();

        public Task<Order> GetOrderByIdAsync(int orderId);

        public Task<IEnumerable<Order>> GerAllUserOrdersAsync(int userId);

        public Task<Order> CreateOrderAsync(int cartId);

        public Task<Order> ApproveOrderAsync(int orderId);

        public Task<bool> DeleteOrderDyIdAsync(int orderId); 
    }
}
