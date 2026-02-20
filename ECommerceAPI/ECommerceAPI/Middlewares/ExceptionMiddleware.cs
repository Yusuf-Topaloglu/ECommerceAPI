using ECommerceAPI.Exceptions;
using ECommerceAPI.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ECommerceAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
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
