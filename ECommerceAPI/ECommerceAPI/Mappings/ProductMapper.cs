using ECommerceAPI.Models.Dtos.Product;
using ECommerceAPI.Models.Entities;

namespace ECommerceAPI.Mappings
{
    public static class ProductMapper
    {
        public static Product ToEntity(CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }

        public static void UpdateEntity(Product product, UpdateProductDto dto)
        {
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
        }
    }
}

