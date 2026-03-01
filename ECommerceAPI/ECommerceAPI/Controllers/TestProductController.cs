using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Responses;
using ECommerceAPI.Services.Abstract;
using ECommerceAPI.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class TestProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<TestProductController> _logger;

        public TestProductController(IProductService productService,ILogger<TestProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll Products request received");
            var hepsiniGetir = await _productService.GetAllProductAsync();
            _logger.LogInformation("GetAll Products completed. Count:{Count}", hepsiniGetir.Count);
            return Ok(hepsiniGetir);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var dto = new ProductDto // Şimdilik mapper olmadığından manuel map edildiği çünkü controller dto döner 
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Ürün getirildi",
                Data = dto
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto productDto)
        {

            _logger.LogInformation("Created products from received");
            await _productService.CreateProductAsync(productDto);
            _logger.LogInformation("Created product from succes");


            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Ürün eklendi",
                Data = productDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {

            var existingProduct = await _productService.UpdateProductAsync(id, dto);

            var responseDto = new ProductDto
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Ürün güncellendi",
                Data = responseDto
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleteProduct = await _productService.DeleteProductAsync(id);

            return Ok();
        }
    }
}