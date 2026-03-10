using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerceAPI.Services.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetByIdAsync(int id);

        Task<int> CreateProductAsync(CreateProductDto dto);

        Task<bool> UpdateProductAsync(int id, UpdateProductDto dto);

        Task<bool> DeleteProductAsync(int id);
    }
}
