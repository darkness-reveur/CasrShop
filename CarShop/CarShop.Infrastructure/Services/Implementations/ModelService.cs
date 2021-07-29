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
    public class ModelService : IModelService
    {
        private readonly CarShopContext _shopContext;

        private readonly ILogger<ModelService> _logger;

        public ModelService(CarShopContext shopContext, ILogger<ModelService> logger)
        {
            _logger = logger;
            _shopContext = shopContext;
        }

        public async Task<CarModel> AddNewCarModelAsync(CarModel newModel)
        {
            if(newModel.Id != 0)
            {
                return null;
            }

            try
            {
                var existinModel = await _shopContext.Models
                    .FirstOrDefaultAsync(model => model.Name == newModel.Name);

                if(existinModel is null)
                {
                    await _shopContext.Models.AddAsync(newModel);

                    await _shopContext.SaveChangesAsync();

                    return newModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(ModelService),
                   nameof(AddNewCarModelAsync),
                   $"Failed to add new model: {newModel.CarBrand.Name} \"{newModel.Name}\"",
                   ex);

                return null;
            }
        }

        public async Task<bool> DeleteCarModelAsync(int modelId)
        {
            try
            {
                var exModel = await _shopContext.Models
                    .AsNoTracking()
                    .FirstOrDefaultAsync(model => model.Id == modelId);

                _shopContext.Models
                    .Remove(exModel);

                await _shopContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(DeleteCarModelAsync),
                    $"Failed to delete model with Id: {modelId} ",
                    ex);

                return false;
            }
        }
            
        public async Task<IEnumerable<CarModel>> GetAllCarModelsAsync()
        {
            try
            {
                var carModel = await _shopContext.Models
                    .Include(model => model.CarBrand)
                    .ToListAsync();

                return carModel;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ModelService),
                    nameof(GetAllCarModelsAsync),
                    $"Cannot get datas about CarModels from database ",
                    ex);

                return null;
            }
        }

        public async Task<CarModel> UpdateCarModelAsync(CarModel model)
        {
            try
            {
                var exModel = await _shopContext.Models
                    .FirstOrDefaultAsync(model => model.Id == model.Id);

                if (!(exModel is null))
                {
                    exModel.Name = model.Name;
                    exModel.CarBrandId = model.CarBrandId;
                }

                await _shopContext.SaveChangesAsync();

                return exModel;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ModelService),
                    nameof(UpdateCarModelAsync),
                    $"Failed to update model with Id: {model.Id}",
                    ex);

                return null;
            }
        }
    }
}
