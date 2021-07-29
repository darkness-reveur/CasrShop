using CarShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface IModelService
    {
        public Task<CarModel> AddNewCarModelAsync(CarModel model);

        public Task<IEnumerable<CarModel>> GetAllCarModelsAsync();
        
        public Task<bool> DeleteCarModelAsync(int modelId);

        public Task<CarModel> UpdateCarModelAsync(CarModel model);

    }
}
