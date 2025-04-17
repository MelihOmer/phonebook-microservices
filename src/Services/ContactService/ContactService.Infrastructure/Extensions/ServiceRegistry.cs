using ContactService.Application.Interfaces;
using ContactService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Infrastructure.Extensions
{
    public static class ServiceRegistry
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("default"));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
        }
    }
}
