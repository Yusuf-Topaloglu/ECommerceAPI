using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models.Dtos.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Bu alan zorunludur")]
        [StringLength(10,ErrorMessage ="En fazla 10 karakter olabilir. ")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01,double.MaxValue,ErrorMessage ="Girilen sayı 0 dan büyük olmalıdır")]
        public decimal Price { get; set; }
        
        public int Stock { get; set; }
    }
}
