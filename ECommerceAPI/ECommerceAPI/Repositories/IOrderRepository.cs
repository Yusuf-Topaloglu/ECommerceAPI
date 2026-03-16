using ECommerceAPI.Models.Entities;

namespace ECommerceAPI.Repositories
{
    public interface IOrderRepository :IRepository<Order>
    {
        Task<List<Order>> GetByUserIdAsync(string userId);

    }
}
