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
        public ActionResult<List<CarModel>> GetAllModels()
        {
            var models = _modelService.GetAllCarModels();

            if (models == null)
            {
                return BadRequest();
            }

            return Ok(models);
        }
        
        [HttpPut]
        public ActionResult<CarModel> UpdateModel([FromBody] CarModel newModel)
        {
            var model = _modelService.
                UpdateCarModel(newModel);

            if (model is null)
            {
                return BadRequest();
            }

            return Ok(model);
        }

        [HttpDelete]
        public ActionResult<CarModel> DeleteCategory(int categoryId)
        {
            var isDeleted = _modelService
                .DeleteCarModel(categoryId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok(isDeleted);
        }
    }
}
