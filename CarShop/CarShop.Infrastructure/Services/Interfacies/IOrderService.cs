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
        public IEnumerable<Order> GetAllOrders();

        public Order GetOrderById(int orderId);

        public Order AddNewOrder(Order newOrder);

        public Cart AddCarToCard(
            int carId,
            int userId);

        public Order UpdateOrder(Order newOrder);

        public bool DeleteOrderDyId(int orderId); 
        
        public bool DeleteCarOutCard(
             int carId,
             int userId);
    }
}
