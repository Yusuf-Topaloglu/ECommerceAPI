using ECommerceAPI.Data;
using ECommerceAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ECommerceContext _context;

        public OrderRepository(ECommerceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}

