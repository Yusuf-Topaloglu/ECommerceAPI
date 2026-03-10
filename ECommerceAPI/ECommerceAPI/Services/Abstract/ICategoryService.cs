using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Models.Entities;

namespace ECommerceAPI.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task<Category?> GetByIdCategoryAsync(int id);

        Task<Category> CreateCategoryAsync(CreateCategoryDto dto);

        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
