using ECommerceAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace ECommerceAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {


            try
            {
                await _requestDelegate(httpContext); 
            }
            catch (Exception ex )
            {

                httpContext.Response.ContentType = "application/json";

                int statusCode = 500; 
                string message = "Beklenmeyen bir hata oluştu";

                if (ex is BaseException baseException)
                {
                    statusCode = baseException.StatusCode;
                    message = baseException.Message;
                }

                httpContext.Response.StatusCode = statusCode;

                var response = new
                {
                    success = false,
                    message
                };

                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }

    }
}
