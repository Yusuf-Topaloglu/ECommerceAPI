using ECommerceAPI.Exceptions;
using ECommerceAPI.Models.Entities;
using ECommerceAPI.Models.Enums;
using ECommerceAPI.Repositories;
using ECommerceAPI.Services.Abstract;

namespace ECommerceAPI.Services.Concrete
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<OrderServices> _logger;

        public OrderServices(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            ILogger<OrderServices> logger)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(string userId)
        {
            var cartItems = await _cartRepository.GetByUserIdAsync(userId);

            if (cartItems == null || !cartItems.Any())
                throw new InvalidOperationException("Sepette ürün bulunmamaktadır");

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    TotalPrice = item.Quantity * item.Product.Price
                };

                orderItems.Add(orderItem);
                totalAmount += orderItem.TotalPrice;
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending,
                OrderItems = orderItems
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveAsync();

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new NotFoundException("Sipariş bulunamadı");

            return order;
        }

       
        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);

            if (orders == null || !orders.Any())
            {
                return new List<Order>();
            }
            return orders;
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, OrderStatus newStatus)
        {
            var orderUpdate = await _orderRepository.GetByIdAsync(id);

            if (orderUpdate==null)
            {
                throw new NotFoundException("Sipariş bulunamadı");
            }
            orderUpdate.Status= newStatus;
            await _orderRepository.SaveAsync();
            return true;

        }
    }
}