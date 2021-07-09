using CarShop.Common.Models;
using System.Collections.Generic;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface IBrandService
    {
        public CarBrand AddNewCarBrand(CarBrand brand);

        public IEnumerable<CarBrand> GetAllCarBrands();

        public bool DeleteCarBrand(int brandId);

        public CarBrand UpdateCarBrand(CarBrand brand);
    }
}
