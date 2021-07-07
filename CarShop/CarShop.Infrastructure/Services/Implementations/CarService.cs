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
    public class CarService : ICarService
    {
        private readonly CarShopContext _shopContext;

        private readonly ILogger<CarService> _logger;

        public CarService(CarShopContext shopContext, ILogger<CarService> logger)
        {
            _logger = logger;
            _shopContext = shopContext;
        }

        public IEnumerable<Car> GetAllCars()
        {
            try
            {
                var Cars = _shopContext.Cars
                    .Include(item => item.CarModel)
                    .ThenInclude(item => item.CarBrand)
                    .Where(item => item.OrderId == null)
                    .ToList();

                return Cars;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(GetAllCars),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public void AddCar(Car newCar)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(Car newCar)
        {
            throw new NotImplementedException();
        }

        public Car GetCarById(int id)
        {
            throw new NotImplementedException();
        }

        public Car GetCarByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(Car newCar)
        {
            throw new NotImplementedException();
        }
    }
}
