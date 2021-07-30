using CarShop.Common.Models;
using CarShop.Common.Models.AuthenticationModels;
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

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CarBrand> Brands { get; set; }

        public DbSet<CarModel> Models { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AccessDataEntity> AccessData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasData(
            //    new User
            //    {
            //        Id = 1,
            //        Age = 20,
            //        Email = "uvapuvp@mail.com",
            //        Role = User.UserRoles.Admin,
            //        MobilePhoneNumber = "375293336651",
            //        Name = "Vladislau",
            //        Cart = new Cart
            //        {
            //            Id = 1,
            //            UserId = 1
            //        },
            //        CartId = 1,

            //    });

            //modelBuilder.Entity<AccessDataEntity>()
            //    .HasData(
            //    new AccessDataEntity
            //    {
            //        Id = 1,
            //        Login = "21232F297A57A5A743894A0E4A801F",
            //        Password = "21232F297A57A5A743894A0E4A801F",
            //        UserId = 1
            //    });

            //modelBuilder.Entity<CarBrand>()
            //    .HasData(
            //    new CarBrand
            //    {
            //        Id = 1,
            //        Name = "Citroen",
            //    });

            //modelBuilder.Entity<CarModel>()
            //    .HasData(
            //    new CarModel
            //    {
            //        Id = 1,
            //        Name = "C4 picasso",
            //        CarBrandId = 1,
            //    }); 
            
            //modelBuilder.Entity<Car>()
            //     .HasData(
            //     new Car
            //     {
            //         Id =1,
            //         CarModelId = 1,
            //         CartId = 1,
            //         Description = "Some car",
            //         EngineType = Car.EngineTypes.PetrolEngine,
            //         EngineVolume = 1.6,
            //         PictureLinks = "https://i.pinimg.com/originals/a6/19/8b/a6198b42200e07da77108dd5dc9c5fdf.jpg",
            //         Price = 6400,
            //         ReleaseYear = 2010,
            //         VehicleMileage = 200000,
            //         WheelDrive = Car.WheelDrives.RealWheelDrive,
            //     });
        }
    }
}
