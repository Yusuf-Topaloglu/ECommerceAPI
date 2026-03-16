using ECommerceAPI.Models.Enums;
using ECommerceAPI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class TestOrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly ILogger<TestOrderController> _logger;

        public TestOrderController(IOrderServices orderServices, ILogger<TestOrderController> logger)
        {
            _orderServices = orderServices;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string userId)
        {
            _logger.LogInformation("Created orders from received");
            var orders = await _orderServices.CreateOrderAsync(userId);
            _logger.LogInformation("Created orders from succes");

            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var ordersGet = await _orderServices.GetOrderByIdAsync(id);

            return Ok(ordersGet);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(string userId)
        {
            var order = await _orderServices.GetUserOrdersAsync(userId);

            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus orderStatus)
        {
            var existingOrders = await _orderServices.UpdateOrderStatusAsync(id, orderStatus);

            return Ok(existingOrders);

        }
    }
}
