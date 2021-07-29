using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly CarShopContext _shopContext;

        private readonly ILogger<BrandService> _logger;

        public BrandService(
            CarShopContext shopContext,
            ILogger<BrandService> logger)
        {
            _logger = logger;
            _shopContext = shopContext;
        }

        public async Task<CarBrand> AddNewCarBrandAsync(CarBrand newBrand)
        {
            if (newBrand.Id != 0)
            {
                return null;
            }

            try
            {
                var existinBrand = await _shopContext.Models
                    .FirstOrDefaultAsync(model => model.Name == newBrand.Name);

                if (existinBrand is null)
                {
                    await _shopContext.Brands.AddAsync(newBrand);

                    await _shopContext.SaveChangesAsync();

                    return newBrand;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(BrandService),
                   nameof(AddNewCarBrandAsync),
                   $"Failed to add new model: \"{newBrand.Name}\"",
                   ex);

                return null;
            }
        }

        public async Task<bool> DeleteCarBrandAsync(int brandId)
        {
            try
            {
                var exBrand = await _shopContext.Brands
                    .AsNoTracking()
                    .FirstOrDefaultAsync(brand => brand.Id == brandId);

                _shopContext.Brands
                    .Remove(exBrand);

                await _shopContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(DeleteCarBrandAsync),
                    $"Failed to delete brand with Id: {brandId} ",
                    ex);

                return false;
            }
        }

        public async Task<IEnumerable<CarBrand>> GetAllCarBrandsAsync()
        {
            try
            {
                var carBrands = await _shopContext.Brands
                    .ToListAsync();

                return carBrands;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(GetAllCarBrandsAsync),
                    $"Cannot get datas about CarBrands from database !",
                    ex);

                return null;
            }
        }

        public async Task<CarBrand> UpdateCarBrandAsync(CarBrand brand)
        {
            try
            {
                var exBrand = await _shopContext.Brands
                    .FirstOrDefaultAsync(brand => brand.Id == brand.Id);

                if (!(exBrand is null))
                {
                    exBrand.Name = brand.Name;
                }

                await _shopContext.SaveChangesAsync();

                return exBrand;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(UpdateCarBrandAsync),
                    $"Failed to update brand with Id: {brand.Id}",
                    ex);

                return null;
            }
        }
    }
}
