using ContactService.Application.Interfaces.Repositories;
using ContactService.Application.Interfaces.Services;
using ContactService.Infrastructure.Persistence;
using ContactService.Infrastructure.Repositories;
using ContactService.Infrastructure.Repository;
using ContactService.Infrastructure.Services;
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
            services.AddScoped<IContactRepository,ContactRepository>();

            services.AddScoped<IContactService, ContactService.Infrastructure.Services.ContactService>();
        }
    }
}
