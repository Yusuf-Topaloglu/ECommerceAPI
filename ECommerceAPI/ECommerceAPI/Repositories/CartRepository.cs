using ECommerceAPI.Data;
using ECommerceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceContext _context;

        public CartRepository(ECommerceContext context)
        {
                _context = context;
        }
        public async Task<CartItem?> GetByProductAndUserAsync(int productId, string userId)
        {
            return await _context.CartItems
               .Include(x => x.Product)
               .FirstOrDefaultAsync(x =>
                   x.ProductId == productId &&
                   x.UserId == userId
               );

        }

        public async Task<List<CartItem>> GetByUserIdAsync(string userId)
        {
            return await _context.CartItems
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
