using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace ECommerceAPI.Services
{
    public class CartService
    {
        private readonly ECommerceContext _context;

        public CartService(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetAllCartAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem?> GetCartAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            return cartItem;
        }

        public async Task<CartItem> AddToCartAsync(int productId, int quantity, string userId)
        {
            var cartItem = await _context.CartItems
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x =>
                    x.ProductId == productId &&
                    x.UserId == userId
                );

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserId = userId
                };

                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return cartItem;
        }



        public async Task<bool> UpdateCartAsync(CartItem cartItem)
        {
            var existingCart = await _context.CartItems.FindAsync(cartItem.Id);
            if (existingCart == null)
                return false;

            existingCart.Quantity = cartItem.Quantity;
            existingCart.ProductId = cartItem.ProductId;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var existingCart = await _context.CartItems.FindAsync(id);
            if (existingCart == null)
                return false;

            _context.CartItems.Remove(existingCart);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
