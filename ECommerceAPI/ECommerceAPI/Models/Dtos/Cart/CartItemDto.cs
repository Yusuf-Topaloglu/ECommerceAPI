namespace ECommerceAPI.Models.Dtos.Category
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
      
    }
}
