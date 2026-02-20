using ECommerceAPI.Data;
using ECommerceAPI.Exceptions;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Repositories;
using ECommerceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace ECommerceAPI.Services.Concrete
{
    public class ProductService : IProductService
    {
       private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository=repository;

        }

        public async Task<List<Product>> GetAllProductAsync()

        {
            return await _repository.GetAllAsync();

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


            var product = new Product
            {
                Name= dto.Name,
                Price= dto.Price,
                Stock= dto.Stock
            };

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
                existingProduct.Name = dto.Name;
                existingProduct.Price = dto.Price;
                existingProduct.Stock = dto.Stock;


            CheckStock(dto.Stock);

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

     





