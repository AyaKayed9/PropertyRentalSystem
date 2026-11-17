using Azure;
using Azure.Core;
using System.Net;
using System.Text.Json;

namespace PropertyRentalManagementSystem.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware
            (RequestDelegate next, 
            ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {               
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occured");
                if (ex is FluentValidation.ValidationException || ex is ArgumentException) 
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                context.Response.ContentType = "application/json";

                string message = ex switch
                {
                    FluentValidation.ValidationException => ex.Message,
                    ArgumentException => ex.Message,
                    _ => "An unexpected error occurred."
                };

                await context.Response.WriteAsJsonAsync(new
                {
                    message
                });

            }
        }
    }
}
