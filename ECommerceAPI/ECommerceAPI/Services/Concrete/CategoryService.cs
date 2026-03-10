using ECommerceAPI.Data;
using ECommerceAPI.Exceptions;
using ECommerceAPI.Mappings;
using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Entities;
using ECommerceAPI.Repositories;
using ECommerceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;

        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category?> GetByIdCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException("Bulunamadı");
            }
            return category;
        }
        public async Task<Category> CreateCategoryAsync(CreateCategoryDto dto)
        {
            ValidateCategory(dto);

            var category = CategoryMapper.ToEntity(dto);
           await _repository.AddAsync(category);
            await _repository.SaveAsync();

            return category;
        }
        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)

        {
            var existingCategory = await _repository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new ValidationException("Kategori bulunmamaktadır");
            if(existingCategory!=null)
            CategoryMapper.UpdateEntity(existingCategory, dto);

            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)

        {
            var existingCategory = await _repository.GetByIdAsync(id);
            if (existingCategory == null)
                throw new ValidationException("Kategori bulunmamaktadır");

            _repository.Delete(existingCategory);
            await _repository.SaveAsync();
            return true;
        }

        private void ValidateCategory(CreateCategoryDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                throw new ValidationException("Name alanı boş veya boşluk bulunamaz");
            }
            if (categoryDto.Name.Length > 100)
            {
                throw new ValidationException("Name 100 karakterden fazla olamaz");
            }

            if (categoryDto.Description!=null && categoryDto.Description.Length>1000)
            {
                throw new ValidationException("Description alanı 1000 den fazla olamaz");
            }
        }
    }
}
