
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanCommerce.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg=> {
                cfg.AddMaps(typeof(ServiceRegistration).Assembly);
            });

            services.AddScoped<ICategoryService, ICategoryService>();
            services.AddScoped<IProductService, IProductService>();

            return services;
        }
    }
}
