using CleanCommerce.Application.Interfaces.Services;
using CleanCommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCommerce.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg=> {
                cfg.AddMaps(typeof(ServiceRegistration).Assembly);
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
