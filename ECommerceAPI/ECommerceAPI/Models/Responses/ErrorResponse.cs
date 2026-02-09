namespace ECommerceAPI.Models.Responses
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public object? Errors { get; set; }

        public ErrorResponse(string message, object? errors = null)
        {
            Message = message;
            Errors = errors;
        }
    }
}
