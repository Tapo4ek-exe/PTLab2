using EShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Db");
            services.AddDbContext<EShopDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IEShopDbContext>(provider =>
                provider.GetService<EShopDbContext>());

            return services;
        }
    }
}
