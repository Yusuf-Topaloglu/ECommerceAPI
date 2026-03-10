using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Models.Entities;

namespace ECommerceAPI.Services.Abstract
{
    public interface ICartService
    {
        Task<List<CartItem>> GetAllCartAsync();

        Task<CartItem?> GetCartAsync(int id);

        Task<CartItem> AddToCartAsync(int productId, int quantity, string userId);

        Task<bool> UpdateCartAsync(int id, CartItemDto cartItemDto);

        Task<bool> RemoveFromCartAsync(int id);
        Task<decimal> GetCartTotalAsync(string userId);
    }
}
