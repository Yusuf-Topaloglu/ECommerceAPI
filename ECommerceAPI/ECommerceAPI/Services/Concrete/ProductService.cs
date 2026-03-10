using ECommerceAPI.Data;
using ECommerceAPI.Exceptions;
using ECommerceAPI.Mappings;
using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Entities;
using ECommerceAPI.Repositories;
using ECommerceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace ECommerceAPI.Services.Concrete
{
    public class ProductService : IProductService
    {
       private readonly IRepository<Product> _repository;
       private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<Product> repository,ILogger<ProductService> logger)
        {
            _repository=repository;
            _logger=logger;

        }

        public async Task<List<Product>> GetAllProductAsync()

        {
            _logger.LogInformation("Fetching all products from repository ");
            var products= await _repository.GetAllAsync();

            _logger.LogInformation("Retrieved {Count} products from database", products.Count);

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Ürün bulunamadı");

            return product; // burada direkt ok çağrılabilir bu doğru ise return product 200 olarak bize dönecek
        }

        public async Task<int> CreateProductAsync(CreateProductDto dto)
        {
            ValidateProduct(dto);
            _logger.LogInformation("New produtcs builder ");

            var product=ProductMapper.ToEntity(dto);
            _logger.LogInformation("New produtcs builder from succes");
            await _repository.AddAsync(product);
            await _repository.SaveAsync();

            return product.Id;

        }

        public async Task<bool> DeleteProductAsync(int id)

        {
            var existingProduct = await _repository.GetByIdAsync(id);

            if (existingProduct == null)
                return false;

           _repository.Delete(existingProduct);
            await _repository.SaveAsync();
            return true;
        }
       
       
       public  async Task<bool> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);

            if (existingProduct == null)
                throw new ValidationException("Böyle bir ürün bulunmamaktadır.");


            CheckStock(dto.Stock);
            ProductMapper.UpdateEntity(existingProduct, dto);



            await _repository.SaveAsync();
            return true;
           
        }

        private void  ValidateProduct(CreateProductDto productDto)
        {
            if (productDto.Price<=0)
            {
                throw new ValidationException("Fiyat sıfırdan büyük olmalıdır");
            }
            if (productDto.Stock <0)
            {
                throw new ValidationException("Stok negatif olamaz");
            }
        }

        private void CheckStock(int stock)
        {
            if (stock<=0)
            {
                throw new ValidationException("Stok sıfır veya negatif olamaz");
            }
        }

        
    }

}

     





