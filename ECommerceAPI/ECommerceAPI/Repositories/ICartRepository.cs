using ECommerceAPI.Data;
using ECommerceAPI.Models.Entities;

namespace ECommerceAPI.Repositories
{
    public interface ICartRepository
    {
       

        Task<CartItem?> GetByProductAndUserAsync(int productId, string userId);

        Task<List<CartItem>> GetByUserIdAsync(string userId);
    }
}
