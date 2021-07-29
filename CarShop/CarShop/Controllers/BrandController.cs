using CarShop.Common.Models;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        private readonly ILogger<BrandController> _logger;

        public BrandController(
            IBrandService brandService,
            ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService
                .GetAllCarBrandsAsync();

            if (brands is null)
            {
                return BadRequest();
            }

            return Ok(brands);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBrand([FromBody] CarBrand newBrand)
        {
            var brand = await _brandService
                .AddNewCarBrandAsync(newBrand);

            if (brand is null)
            {
                return BadRequest();
            }

            return Ok(brand);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] CarBrand newBrand) 
        {
            var brand = await _brandService
                .UpdateCarBrandAsync(newBrand);

            if (newBrand is null)
            {
                return BadRequest();
            }

            return Ok(newBrand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            var isDeleted = await _brandService
                .DeleteCarBrandAsync(brandId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(isDeleted);
        }
    }
}
