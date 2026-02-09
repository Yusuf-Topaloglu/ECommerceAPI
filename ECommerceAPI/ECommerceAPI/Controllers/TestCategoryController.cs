using ECommerceAPI.Models;
using ECommerceAPI.Models.Responses;
using ECommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models.Dtos.Category;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class TestCategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public TestCategoryController(CategoryService categoryService)
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

            if (category == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Kategori bulunamadı",
                    Data = null
                });

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

        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var create = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            await _categoryService.CreateCategory(createCategoryDto);

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

            if (!existingCategory)
                return NotFound();

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

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
