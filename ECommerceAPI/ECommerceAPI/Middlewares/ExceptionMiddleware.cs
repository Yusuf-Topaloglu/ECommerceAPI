using ECommerceAPI.Exceptions;
using ECommerceAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ECommerceAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;


        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception occured, Path {Path}", context.Request.Path);
                context.Response.ContentType = "application/json";

                int statusCode = 500;
                string message = "Beklenmeyen bir hata oluştu";

                if (ex is BaseException baseException)
                {
                    statusCode = baseException.StatusCode;
                    message = baseException.Message;
                }

                context.Response.StatusCode = statusCode;

                var errorResponse = new ErrorResponse(message);

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(errorResponse)
                );
            }
        }
    }
}
