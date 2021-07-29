using CarShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();

        Task<Car> GetCarByIdAsync(int id);

        Task<Car> AddCarAsync(Car newCar);

        Task<Car> UpdateCarAsync(Car newCar);

        Task<bool> DeleteCarAsync(int carId);
    }
}
