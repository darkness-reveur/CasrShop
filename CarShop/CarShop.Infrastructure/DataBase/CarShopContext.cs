using CarShop.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.DataBase
{
    public class CarShopContext : DbContext
    {
        public CarShopContext(DbContextOptions<CarShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarBrand> Brands { get; set; }

        public DbSet<CarModel> Models { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
