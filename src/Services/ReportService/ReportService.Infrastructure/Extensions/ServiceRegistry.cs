using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Infrastructure.Repositories.ReportRepositories;
using ReportService.Application.Mappings;
using ReportService.Application.Interfaces.Services;

namespace ReportService.Infrastructure.Extensions
{
    public static class ServiceRegistry
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReportDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("postgres"));
            });
            services.AddAutoMapper(typeof(ReportMapperProfile).Assembly);

            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportService, ReportService.Infrastructure.Services.ReportService>();
        }
    }
}
