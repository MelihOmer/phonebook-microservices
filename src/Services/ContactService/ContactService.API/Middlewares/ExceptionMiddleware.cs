using FluentValidation;
using PhonebookMicroservices.Shared.Exceptions;
using PhonebookMicroservices.Shared.ResponseTypes;
using System.Diagnostics;

namespace ContactService.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception Occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            var response = ex switch
            {
                NotFoundException notFoundException =>
                    ApiResponse<object>.Fail("Kaynak bulunamadı.",traceId,notFoundException.Message ),

                _ =>
                    ApiResponse<object>.Fail("Beklenmeyen bir hata oluştu.",traceId,ex.Message)
            };
            
            await context.Response.WriteAsJsonAsync(response);
        }
        
    }
}
