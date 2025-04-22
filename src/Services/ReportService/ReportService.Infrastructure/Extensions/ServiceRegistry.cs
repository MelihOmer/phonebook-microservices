using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Application.Interfaces.Services;
using ReportService.Application.Mappings;
using ReportService.Infrastructure.Persistence;
using ReportService.Infrastructure.Repositories.ReportDetailsRepositories;
using ReportService.Infrastructure.Repositories.ReportRepositories;
using ReportService.Infrastructure.Services;

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
            services.AddScoped<IReportDetailsRepository, ReportDetailRepositories>();


            services.AddScoped<IReportService, ReportService.Infrastructure.Services.ReportService>();
            services.AddScoped<IReportDetailService, ReportDetailService>();

            
        }
    }
}
