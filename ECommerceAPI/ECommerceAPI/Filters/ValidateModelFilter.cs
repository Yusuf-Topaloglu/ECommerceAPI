using ECommerceAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Filters
{
    public class ValidateModelFilter : IActionFilter
    {
        private readonly ILogger<ValidateModelFilter> _logger;

        public ValidateModelFilter(ILogger<ValidateModelFilter> logger)
        {
            _logger = logger;  
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                var errorMessages = string.Join(",",context.ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage));
                _logger.LogWarning("Validation failed for {Controller}.{Action}. Errors: {Errors}",context.Controller.GetType().
                    Name,context.ActionDescriptor.DisplayName,errorMessages);
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value.Errors.Select(e => e.ErrorMessage)
                    );

                var response = new ErrorResponse(
                    "Validation hatası",
                    errors
                );

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
