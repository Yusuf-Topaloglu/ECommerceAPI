using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Cart;
using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Services.Abstract;
using ECommerceAPI.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class TestCartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public TestCartController(ICartService cartService)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartAsync(int id,CartItemDto cartItemDto)
        {
            var updateCart= await _cartService.UpdateCartAsync(id, cartItemDto);
            

            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartAsync(int id)
        {
            var deleteCart = await _cartService.RemoveFromCartAsync(id);
            

            return Ok();

        }
    }

}
