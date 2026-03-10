using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Entities;

namespace ECommerceAPI.Mappings
{
    public static class CategoryMapper
    {
        public static  Category ToEntity(CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };
               
        }
        public static void UpdateEntity(Category category, UpdateCategoryDto dto)
        {
            category.Name = dto.Name;
            category.Description = dto.Description;
        }

    }
}
