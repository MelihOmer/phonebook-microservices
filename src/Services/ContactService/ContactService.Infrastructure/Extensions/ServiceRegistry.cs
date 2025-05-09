﻿using ContactService.Application.Interfaces.Repositories;
using ContactService.Application.Interfaces.Services;
using ContactService.Infrastructure.Persistence;
using ContactService.Infrastructure.Repositories;
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
                opt.UseNpgsql(configuration.GetConnectionString("postgres"));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
            services.AddScoped<IContactRepository,ContactRepository>();
            services.AddScoped<IContactInformationRepository, ContactInformationRepository>();

            services.AddScoped<IContactService, ContactService.Infrastructure.Services.ContactService>();
            services.AddScoped<IContactInformationService, ContactInformationService>();

            services.AddScoped<IStatisticService,StatisticService>();
        }
    }
}
