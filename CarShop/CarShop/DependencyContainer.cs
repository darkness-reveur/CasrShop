using CarShop.Infrastructure.Services.AuthenticationService;
using CarShop.Infrastructure.Services.Implementations;
using CarShop.Infrastructure.Services.Interfacies;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop
{
    public static class DependencyContainer
    {
        public static void RegisterDependesy(this IServiceCollection service)
        {
            service.AddTransient<ICarService, CarService>();

            service.AddTransient<IModelService, ModelService>();

            service.AddTransient<IBrandService, BrandService>();

            service.AddTransient<IUserService, UserService>();

            service.AddTransient<ICartService, CartService>();

            service.AddTransient<IOrderService, OrderService>();

            service.AddTransient<IAuthService, AuthService>();
        }
    }
}
