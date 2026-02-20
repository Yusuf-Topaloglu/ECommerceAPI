using ECommerceAPI.Data;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface ICartRepository
    {
       

        Task<CartItem?> GetByProductAndUserAsync(int productId, string userId);
    }
}
