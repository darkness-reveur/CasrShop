using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            try
            {
                var Cars = await _shopContext.Cars
                    .Include(car => car.User)
                    .Include(car => car.CarModel)
                    .ThenInclude(model => model.CarBrand)
                    .ToListAsync();

                return Cars;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(GetAllCarsAsync),
                    $"Cannot get data from database",
                    ex);

                return null;
            }
        }

        public async Task<Car> AddCarAsync(Car newCar)
        {
            if (newCar.Id != 0)
            {
                return null;
            }
            try
            {
                var existinCar = await _shopContext.Cars
                    .FirstOrDefaultAsync(car => car.UserId == newCar.UserId
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
                    await _shopContext.Cars
                        .AddAsync(newCar);

                    await _shopContext.SaveChangesAsync();
                }

                return newCar;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(AddCarAsync),
                    $"Failed to add new car: {newCar.CarModel.CarBrand} {newCar.CarModel} {newCar.ReleaseYear}" +
                    $"\n{newCar.User.Name} {newCar.User.Email} Trying to add car",
                    ex);

                return null;
            }
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            if (carId == 0)
            {
                return false;
            }

            try
            {
                var exCar = await _shopContext.Cars
                    .AsNoTracking()
                    .FirstOrDefaultAsync(car => car.Id == carId);

                _shopContext.Cars.Remove(exCar);

                await _shopContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(DeleteCarAsync),
                    $"Failed to delete car with Id: {carId} ",
                    ex);

                return false;
            }
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            try
            {
                var car = await _shopContext.Cars
                    .Include(car => car.CarModel)
                    .ThenInclude(model => model.CarBrand)
                    .Include(car => car.User)
                    .FirstOrDefaultAsync(car => car.Id == id);

                return car;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(GetCarByIdAsync),
                    $"Cannot get car with Id: {id} ",
                    ex);

                return null;
            }
        }

        public async Task<Car> UpdateCarAsync(Car newCar)
        {
            try
            {
                var exCar = await _shopContext.Cars
                    .FirstOrDefaultAsync(car => car.Id == newCar.Id);

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

                await _shopContext.SaveChangesAsync();

                return exCar;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(UpdateCarAsync),
                    $"Failed to update new car: {newCar.CarModel.CarBrand} {newCar.CarModel} {newCar.ReleaseYear}",
                    ex);

                return null;
            }
        }
    }
}
