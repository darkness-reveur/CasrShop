using CarShop.Common.Models;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly ICarService _carService;

        public ProductController(
            ILogger<ProductController> logger,
            ICarService carService)
        {
            _carService = carService;

            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService
                .GetAllCarsAsync();

            if (cars is null)
            {
                return BadRequest();
            }

            return Ok(cars);
        }

        [HttpGet]
        [Route("GetCar")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _carService
                .GetCarByIdAsync(id);

            if (car is null)
            {
                return BadRequest();
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] Car newCar)
        {
            var addedCar = await _carService
                .AddCarAsync(newCar);

            if (addedCar is null)
            {
                return BadRequest();
            }

            return Ok(addedCar);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromBody] Car newCar)
        {
            var updatedCar = await _carService
                .UpdateCarAsync(newCar);

            if (updatedCar is null)
            {
                return BadRequest();
            }

            return Ok(updatedCar);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            var isDeleted = await _carService
                .DeleteCarAsync(carId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(isDeleted);
        }
    }
}
