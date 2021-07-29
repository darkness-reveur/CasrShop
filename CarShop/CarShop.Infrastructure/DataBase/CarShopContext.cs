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
            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    Id = 1,
                    Age = 20,
                    Email = "uvapuvp@mail.com",
                    Role = User.UserRoles.Admin,
                    MobilePhoneNumber = "375293336651",
                    Name = "Vladislau",
                });

            modelBuilder.Entity<AccessDataEntity>()
                .HasData(
                new AccessDataEntity
                {
                    Id = 1,
                    Login = "21232F297A57A5A743894A0E4A801F",
                    Password = "21232F297A57A5A743894A0E4A801F",
                    UserId = 1
                });

            modelBuilder.Entity<CarBrand>()
                .HasData(
                new CarBrand
                {
                    Id = 1,
                    Name = "Citroen",
                });

            modelBuilder.Entity<CarBrand>()
                .HasData(
                new CarBrand
                {
                    Id = 1,
                    Name = "Citroen",
                });
        }
    }
}
