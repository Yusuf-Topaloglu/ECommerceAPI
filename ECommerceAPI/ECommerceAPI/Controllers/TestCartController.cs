using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestCartController : ControllerBase
    {
        private readonly CartService _cartService;

        public TestCartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            try
            {
                var cart = await _cartService.GetCartAsync("test-user-123");
                return Ok(new
                {
                    Message = "CartService çalışıyor!",
                    CartItems = cart
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}