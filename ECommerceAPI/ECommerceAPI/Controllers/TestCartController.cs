using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Cart;
using ECommerceAPI.Models.Dtos.Category;
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
        public async Task<IActionResult> GetAllCartAsync()
        {
            var sepet = await _cartService.GetAllCartAsync();
            return Ok(sepet);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCardAsync(int id)
        {
            var existingCart = await _cartService.GetCartAsync(id);

            if (existingCart == null)
                return NotFound();


            return Ok(existingCart);
        }
        [HttpPost]
        public async Task<IActionResult> AddCart(AddToCartDto dto)
        {

            string userId = "test-user";

            var cartItem = await _cartService.AddToCartAsync(dto.ProductId,dto.Quantity, userId);

            var response = new CartItemDto
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                ProductName = cartItem.Product.Name,
                ProductPrice = cartItem.Product.Price
            };
            return Ok(response);
            
        }
        [HttpPut("{cartItem}")]
        public async Task<IActionResult> UpdateCartAsync(CartItem cartItem)
        {
            var updateCart= await _cartService.UpdateCartAsync(cartItem);
            if (!updateCart)
                return NotFound();

            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartAsync(int id)
        {
            var deleteCart = await _cartService.RemoveFromCartAsync(id);
            if (!deleteCart)
                return NotFound();

            return Ok();

        }
    }

}
