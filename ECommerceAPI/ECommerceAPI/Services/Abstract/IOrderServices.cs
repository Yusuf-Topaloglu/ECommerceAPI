using ECommerceAPI.Models.Entities;
using ECommerceAPI.Models.Enums;

namespace ECommerceAPI.Services.Abstract
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string userId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int id, OrderStatus newStatus);
    }
}
