using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Responses;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestProductController :ControllerBase
    {
        private readonly ProductService _productService;

        public TestProductController(ProductService productService)
        {
                _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hepsiniGetir= await _productService.GetAllProductAsync();
            return Ok(hepsiniGetir);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ürün bulunamadı",
                    Data = null
                });

            var dto = new ProductDto
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var product = new Product
            {
                Name = productDto.Name,
                Price=productDto.Price,
                Stock=productDto.Stock
            };

           await _productService.CreateProductAsync(product);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Ürün eklendi",
                Data = productDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id ,UpdateProductDto dto)
        {
         
            var existingProduct = await _productService.UpdateProductAsync(id,dto);
            if(!existingProduct)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ürün Güncellenmedi",
                    Data =null


                    });

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
            if (!deleteProduct)
                return NotFound(new ApiResponse<object>
                {

                    Success = false,
                    Message="Ürün silinemedi",
                    Data=null
                });

            return Ok(new ApiResponse<object>
            {
                Success=true,
                Message="Ürün silindi",
                Data=null
            });
        }
        [HttpGet("calculate-total")]
        public IActionResult CalculateTotal(decimal price, decimal discount, decimal tax)
        {
            var total = _productService.CalculateTotal(price, discount, tax);
            return Ok(total);
        }
        [HttpPost]
        public IActionResult ValidateProduct(Product product)
        {
            if (!_productService.IsValid(product))
                return BadRequest("Ürün bilgileri hatalı");

            return Ok("Ürün geçerli");
        }

        [HttpPost("Stok-Kontrol")]
        public IActionResult HasEnough(int productId, int requestedQuantity)
        {
            var control = _productService.HasEnoughStock(productId,requestedQuantity);
            if (!control)
            {
                return BadRequest("Ürün stokları uyuşmamaktadır.");
            }
          
            return Ok("Stoklar yeterli");
        }

        [HttpPut("Decrease-stock")]
        public IActionResult Decrase(int productId, int quantity)
        {
            var existing= _productService.DecreaseStock(productId,quantity);
            if(!existing)
            {
                return BadRequest("Stok düşürülemedi");
            }
            return Ok("stok Başarıyla güncellendi");
        }
    }

}