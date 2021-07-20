using CarShop.Common.Helpers;
using CarShop.Common.Models;
using CarShop.Infrastructure.DataBase;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public CarBrand AddNewCarBrand(CarBrand newBrand)
        {
            if (newBrand.Id != 0)
            {
                return null;
            }

            try
            {
                var existinBrand = _shopContext.Models
                    .FirstOrDefault(model => model.Name == newBrand.Name);

                if (existinBrand is null)
                {
                    _shopContext.Brands.Add(newBrand);

                    _shopContext.SaveChanges();

                    return newBrand;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                   nameof(BrandService),
                   nameof(AddNewCarBrand),
                   $"Failed to add new model: \"{newBrand.Name}\"",
                   ex);

                return null;
            }
        }

        public bool DeleteCarBrand(int brandId)
        {
            try
            {
                var exBrand = _shopContext.Brands
                    .AsNoTracking()
                    .FirstOrDefault(brand => brand.Id == brandId);

                _shopContext.Brands
                    .Remove(exBrand);

                _shopContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(DeleteCarBrand),
                    $"Failed to delete brand with Id: {brandId} ",
                    ex);

                return false;
            }
        }

        public IEnumerable<CarBrand> GetAllCarBrands()
        {
            try
            {
                var carBrands = _shopContext.Brands
                    .ToList();

                return carBrands;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(GetAllCarBrands),
                    $"Cannot get datas about CarBrands from database !",
                    ex);

                return null;
            }
        }

        public CarBrand UpdateCarBrand(CarBrand brand)
        {
            try
            {
                var exBrand = _shopContext.Brands
                    .FirstOrDefault(brand => brand.Id == brand.Id);

                if (!(exBrand is null))
                {
                    exBrand.Name = brand.Name;
                }

                _shopContext.SaveChanges();

                return exBrand;
            }
            catch (Exception ex)
            {
                _logger.LogErrorByTemplate(
                    nameof(BrandService),
                    nameof(UpdateCarBrand),
                    $"Failed to update brand with Id: {brand.Id}",
                    ex);

                return null;
            }
        }
    }
}
