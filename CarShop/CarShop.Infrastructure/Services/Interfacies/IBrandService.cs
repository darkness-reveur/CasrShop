using CarShop.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Interfacies
{
    public interface IBrandService
    {
        public Task<CarBrand> AddNewCarBrandAsync(CarBrand brand);

        public Task<IEnumerable<CarBrand>> GetAllCarBrandsAsync();

        public Task<bool> DeleteCarBrandAsync(int brandId);

        public Task<CarBrand> UpdateCarBrandAsync(CarBrand brand);
    }
}
