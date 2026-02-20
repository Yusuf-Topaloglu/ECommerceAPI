using ECommerceAPI.Models;
using ECommerceAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models.Dtos.Category;
using ECommerceAPI.Services.Concrete;
using ECommerceAPI.Services.Abstract;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class TestCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public TestCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Tüm kategoriler listelendi
        /// </summary>
        

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var kategorileriGetir = await _categoryService.GetAllCategoryAsync();

            var dtolist = kategorileriGetir.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return Ok(new ApiResponse<List<CategoryDto>>
            {
                Success = true,
                Message = "Kategoriler listelendi",
                Data = dtolist
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var category = await _categoryService.GetByIdCategoryAsync(id);



            var dto = new CategoryDto
            {
                Name = category.Name,
                Id = category.Id,
                Description = category.Description
            };
            return Ok(new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "kategori getirildi",
                Data = dto
            });
        }
        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
           


           

            await _categoryService.CreateCategoryAsync(createCategoryDto);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Kategori eklendi",
                Data = createCategoryDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryDto updateCategoryDto)

        {
            var existingCategory = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);

           

            var responseDto= new CategoryDto
            { 
                 Name=updateCategoryDto.Name,
                 Description=updateCategoryDto.Description

            };

            return Ok(new ApiResponse<object>
            {
                Success=true,
                Message="Kategori güncellendi",
                Data=responseDto
            });
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted= await _categoryService.DeleteCategoryAsync(id);

            return Ok();
        }

    }
}
