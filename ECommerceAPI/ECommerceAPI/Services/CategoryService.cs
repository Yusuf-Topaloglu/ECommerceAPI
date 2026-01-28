using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Models.Dtos.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ECommerceAPI.Services
{
    public class CategoryService
    {
        private readonly ECommerceContext _context;

        public CategoryService(ECommerceContext context)
        {
            _context = context;

        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdCategoryAsync(int id)
        {
            var  category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            return category;
        }
        public async Task<Category> CreateCategory(CreateCategoryDto dto)
        {
            var category = new Category  //automapper olmadığından manuel map oluşturuldu
            {
                Name = dto.Name,
                Description = dto.Description
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }
        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)

        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return false;

            existingCategory.Name = dto.Name;
            existingCategory.Description = dto.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)

        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return false;

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
