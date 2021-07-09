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

        private readonly ILogger<CarService> _logger;

        public ModelService(CarShopContext shopContext, ILogger<CarService> logger)
        {
            _logger = logger;
            _shopContext = shopContext;
        }

        public CarModel AddNewCarModel(CarModel newModel)
        {
            if(newModel.Id != 0)
            {
                return null;
            }

            try
            {
                var existinModel = _shopContext.Models
                    .FirstOrDefault(model => model.Name == newModel.Name);

                if(existinModel is null)
                {
                    _shopContext.Models.Add(newModel);

                    _shopContext.SaveChanges();

                    return newModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(ModelService),
                   nameof(AddNewCarModel),
                   $"Failed to add new model: {newModel.CarBrand.Name} \"{newModel.Name}\"",
                   ex);

                return null;
            }
        }

        public bool DeleteCarModel(int modelId)
        {
            if (modelId == 0)
            {
                return false;
            }

            try
            {
                var exModel = _shopContext.Models
                    .AsNoTracking()
                    .FirstOrDefault(model => model.Id == modelId);

                _shopContext.Models
                    .Remove(exModel);

                _shopContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(CarService),
                    nameof(DeleteCarModel),
                    $"Failed to delete model with Id: {modelId} ",
                    ex);

                return false;
            }
        }

        
        public IEnumerable<CarModel> GetAllCarModels()
        {
            try
            {
                var carModel = _shopContext.Models
                    .Include(model => model.CarBrand)
                    .ToList();

                return carModel;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ModelService),
                    nameof(GetAllCarModels),
                    $"Cannot get data from database ",
                    ex);

                return null;
            }
        }

        public CarModel UpdateCarModel(CarModel model)
        {
            try
            {
                var exModel = _shopContext.Models
                    .FirstOrDefault(model => model.Id == model.Id);

                if (!(exModel is null))
                {
                    exModel.Name = model.Name;
                    exModel.CarBrandId = model.CarBrandId;
                }

                _shopContext.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(ModelService),
                    nameof(UpdateCarModel),
                    $"Failed to update model with Id: {model.Id}",
                    ex);

                return null;
            }
            throw new NotImplementedException();
        }


    }
}
