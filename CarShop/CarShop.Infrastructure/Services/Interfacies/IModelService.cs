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
        public CarModel AddNewCarModel(CarModel model);

        public IEnumerable<CarModel> GetAllCarModels();
        
        public bool DeleteCarModel(int modelId);

        public CarModel UpdateCarModel(CarModel model);

    }
}
