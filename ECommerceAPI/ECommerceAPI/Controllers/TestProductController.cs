using ECommerceAPI.Models;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestProductController : Controller
    {
        private readonly ProductService _productService;

        public TestProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Test ()
        {
            var products= await _productService.GetAllProductAsync(); 
           return Ok (new {Message="ProductService Çalışıyor !",Product = products});
        }
    }
}
