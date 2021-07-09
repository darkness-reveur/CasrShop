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
        IEnumerable<Car> GetAllCars();

        Car GetCarById(int id);

        Car AddCar(Car newCar);

        Car UpdateCar(Car newCar);

        bool DeleteCar(int carId);
    }
}
