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
        public async Task<IActionResult> Test()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(new { Message = "ProductService Çalışıyor !", Product = products });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product =await _productService.GetProductByIdAsync(id);

            if (product == null) 
            
            return NotFound($"ID: {id} olan ürün bulunamadı");
            
            return Ok(new { Message = "Ürün başarılı şekilde geldi", Product = product });
        }
    }
}
