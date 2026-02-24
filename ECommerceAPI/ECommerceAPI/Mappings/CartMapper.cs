using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Category;

namespace ECommerceAPI.Mappings
{
    public static class CartMapper
    {
        public static CartItem ToEntity(int productId, int quantity, string userId)
        {
            return new CartItem
            {

                ProductId = productId,
                Quantity = quantity,
                UserId = userId

            };
        }

        public static void UpdateEntity(CartItem cartItem, CartItemDto cartItemDto)
        {
            cartItem.Quantity = cartItemDto.Quantity;
            cartItem.ProductId = cartItemDto.ProductId;
        }
    }
}
