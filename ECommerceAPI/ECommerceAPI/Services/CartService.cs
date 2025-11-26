using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services
{
    public class CartService
    {
        private readonly ECommerceContext _context;

        public CartService(ECommerceContext context)
        {
            _context = context;
        }

        
        public async Task AddToCartAsync(int productId, int quantity, string userId)
        {
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        
        public async Task<List<CartItem>> GetCartAsync(string userId)
        {
            return await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)
                .ToListAsync();
        }

       
        public async Task UpdateQuantityAsync(int cartItemId, int newQuantity, string userId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem != null)
            {
                cartItem.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task RemoveFromCartAsync(int cartItemId, string userId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}