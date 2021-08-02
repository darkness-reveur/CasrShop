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
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        private readonly ILogger<ModelController> _logger;

        public ModelController(
            IModelService modelService,
            ILogger<ModelController> logger)
        {
            _logger = logger;
            _modelService = modelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var models = await _modelService.GetAllCarModelsAsync();

            if (models == null)
            {
                return BadRequest();
            }

            return Ok(models);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateModel([FromBody] CarModel newModel)
        {
            var model = await _modelService.
                UpdateCarModelAsync(newModel);

            if (model is null)
            {
                return BadRequest();
            }

            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var isDeleted = await _modelService
                .DeleteCarModelAsync(categoryId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(isDeleted);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewModel(CarModel carModel)
        {
            if (carModel is null)
            {
                return BadRequest();
            }

            return Ok(await _modelService.AddNewCarModelAsync(carModel));
        }
    }
}
