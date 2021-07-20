using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    .Include(car => car.User)
                    .Include(car => car.CarModel)
                    .ThenInclude(model => model.CarBrand)
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

        public Car AddCar(Car newCar)
        {
            if (newCar.Id != 0)
            {
                return null;
            }
            try
            {
                var existinCar = _shopContext.Cars
                    .FirstOrDefault(car => car.UserId == newCar.UserId
                    && car.VehicleMileage == newCar.VehicleMileage
                    && car.Description == newCar.Description
                    && car.CarModelId == newCar.CarModelId
                    && car.ReleaseYear == newCar.ReleaseYear
                    && car.Price == newCar.Price
                    && car.WheelDrive == newCar.WheelDrive
                    && car.EngineType == newCar.EngineType
                    && car.EngineVolume == newCar.EngineVolume);

                if (existinCar is null)
                {
                    _shopContext.Cars
                        .Add(newCar);

                    _shopContext.SaveChanges();
                }

                return newCar;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(AddCar),
                    $"Failed to add new car: {newCar.CarModel.CarBrand} {newCar.CarModel} {newCar.ReleaseYear}",
                    ex);

                return null;
            }
        }

        public bool DeleteCar(int carId)
        {
            if (carId == 0)
            {
                return false;
            }

            try
            {
                var exCar = _shopContext.Cars
                    .AsNoTracking()
                    .FirstOrDefault(car => car.Id == carId);

                _shopContext.Cars.Remove(exCar);

                _shopContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(DeleteCar),
                    $"Failed to delete car with Id: {carId} ",
                    ex);

                return false;
            }
        }

        public Car GetCarById(int id)
        {
            try
            {
                var car = _shopContext.Cars
                    .Include(car => car.CarModel)
                    .ThenInclude(model => model.CarBrand)
                    .Include(car => car.User)
                    .FirstOrDefault(car => car.Id == id);

                return car;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(GetCarById),
                    $"Cannot get car with Id: {id} ",
                    ex);

                return null;
            }
        }

        public Car UpdateCar(Car newCar)
        {
            try
            {
                var exCar = _shopContext.Cars
                    .FirstOrDefault(car => car.Id == newCar.Id);

                if (!(exCar is null))
                {
                    exCar.CarModelId = newCar.CarModelId;
                    exCar.Description = newCar.Description;
                    exCar.EngineVolume = newCar.EngineVolume;
                    exCar.Price = newCar.Price;
                    exCar.ReleaseYear = newCar.ReleaseYear;
                    exCar.VehicleMileage = newCar.VehicleMileage;
                    exCar.EngineVolume = newCar.EngineVolume;
                    exCar.WheelDrive = newCar.WheelDrive;
                    exCar.EngineType = newCar.EngineType;
                }

                _shopContext.SaveChanges();

                return exCar;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(UpdateCar),
                    $"Failed to update new car: {newCar.CarModel.CarBrand} {newCar.CarModel} {newCar.ReleaseYear}",
                    ex);

                return null;
            }
        }
    }
}
