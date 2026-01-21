namespace ECommerceAPI.Models.Dtos.Product
{
    public class ProductDto
    {
       
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
      



    }
}
