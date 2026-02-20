using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models.Dtos.Category
{
    public class CartItemDto
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Bu alan zorunludur")]
        [StringLength(50, ErrorMessage="Maksimum 50 karakter girilebilir")]
        public string ProductName { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Girilen sayı 0 dan büyük olmalıdır")]
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
      
    }
}
