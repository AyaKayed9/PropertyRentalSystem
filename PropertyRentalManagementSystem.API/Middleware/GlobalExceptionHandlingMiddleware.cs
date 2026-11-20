
using System.Net;

namespace PropertyRentalManagementSystem.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();

                context.Response.StatusCode = ex switch
                {
                    FluentValidation.ValidationException =>
                        (int)HttpStatusCode.BadRequest,

                    ArgumentException =>
                        (int)HttpStatusCode.BadRequest,

                    KeyNotFoundException =>
                        (int)HttpStatusCode.NotFound,

                    _ =>
                        (int)HttpStatusCode.InternalServerError
                };

                context.Response.ContentType = "application/json";

                string message = ex switch
                {
                    FluentValidation.ValidationException => ex.Message,
                    ArgumentException => ex.Message,
                    KeyNotFoundException => ex.Message,
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
