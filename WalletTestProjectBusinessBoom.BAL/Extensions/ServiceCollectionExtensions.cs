using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.BAL.Services;
using WalletTestProjectBusinessBoom.BAL.Services.Interfaces;
using WalletTestProjectBusinessBoom.DAL;
using WalletTestProjectBusinessBoom.DAL.Repositories;
using WalletTestProjectBusinessBoom.Сore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace WalletTestProjectBusinessBoom.BAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseContext(configuration);
            services.AddRepositories();
            services.AddServices();
            return services;
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
