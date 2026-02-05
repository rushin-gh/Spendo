using System.Net;
using System.Text.Json;

namespace apis.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");
                await HandleException(httpContext, ex);
            }
        }

        private static Task HandleException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            // TODO : Handle ArgumentNullException
            httpContext.Response.StatusCode = exception switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                IsSuccess = false,
                StatusCode = httpContext.Response.StatusCode,
                Message = httpContext.Response.StatusCode >= 500 ? "Internal server error!" : exception.Message
            };

            return httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }
    }
}
