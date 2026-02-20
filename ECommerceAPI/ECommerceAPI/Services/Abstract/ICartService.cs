using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Category;

namespace ECommerceAPI.Services.Abstract
{
    public interface ICartService
    {
        Task<List<CartItem>> GetAllCartAsync();

        Task<CartItem?> GetCartAsync(int id);

        Task<CartItem> AddToCartAsync(int productId, int quantity, string userId);

        Task<bool> UpdateCartAsync(int id, CartItemDto cartItemDto);

        Task<bool> RemoveFromCartAsync(int id);


    }
}
