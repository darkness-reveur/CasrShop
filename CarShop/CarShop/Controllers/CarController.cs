using CarShop.Common.Models;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CarShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;

        private readonly ICarService _carService;

        public CarController(
            ILogger<CarController> logger,
            ICarService carService)
        {
            _carService = carService;

            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCars")]
        public ActionResult<List<Car>> GetAllCars()
        {
            var cars = _carService
                .GetAllCars();

            if (cars is null)
            {
                return BadRequest();
            }

            return Ok(cars);
        }

        [HttpGet]
        [Route("GetCarById")]
        public ActionResult<Car> GetCarById(int id)
        {
            var car = _carService
                .GetCarById(id);

            if (car is null)
            {
                return BadRequest();
            }

            return Ok(car);
        }

        [HttpPost]
        public ActionResult<Car> AddCar([FromBody] Car newCar)
        {
            var addedCar = _carService
                .AddCar(newCar);

            if (addedCar is null)
            {
                return BadRequest();
            }

            return Ok(addedCar);
        }

        [HttpPut]
        public ActionResult<Car> UpdateCar([FromBody] Car newCar)
        {
            var updatedCar = _carService
                .UpdateCar(newCar);

            if (updatedCar is null)
            {
                return BadRequest();
            }

            return Ok(updatedCar);
        }

        [HttpDelete]
        public ActionResult<Car> DeleteCar(int carId)
        {
            var isDeleted = _carService
                .DeleteCar(carId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(isDeleted);
        }
    }
}
