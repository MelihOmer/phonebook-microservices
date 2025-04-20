using ContactService.Application.Mappings;
using ContactService.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PhonebookMicroservices.Shared.ResponseTypes;
using System.Diagnostics;
using System.Reflection;

namespace ContactService.Application.Extensions
{
    public static class ServiceRegistry
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ContactMappingProfile)));
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(ContactCreateDtoValidator).Assembly));
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    string traceId = Activity.Current?.TraceId.ToString() ?? context.HttpContext.TraceIdentifier;
                    var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary
                    (
                        y => y.Key,
                        y => y.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                    var response = ApiResponse<object>.Fail("Bir veya daha fazla Validasyon hatası.", traceId, errors);
                    return new BadRequestObjectResult(response);
                };
            });
        }
        
    }
}
