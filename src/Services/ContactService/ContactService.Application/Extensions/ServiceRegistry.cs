using ContactService.Application.Mappings;
using ContactService.Application.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactService.Application.Extensions
{
    public static class ServiceRegistry
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ContactMappingProfile)));
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(ContactCreateDtoValidator).Assembly));
        }
    }
}
