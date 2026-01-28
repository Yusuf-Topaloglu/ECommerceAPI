using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models.Dtos.Category
{
    public class CreateCategoryDto // Kategori eklerken alınacak veri 
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}
