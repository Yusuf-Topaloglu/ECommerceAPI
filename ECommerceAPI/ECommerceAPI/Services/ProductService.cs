using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Models.Dtos.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using System.Diagnostics.Eventing.Reader;

namespace ECommerceAPI.Services
{
    public class ProductService
    {
        private readonly ECommerceContext _context;

        public ProductService(ECommerceContext context)
        {
            _context = context;

        }

        public async Task<List<Product>> GetAllProductAsync()

        {
            return await _context.Products.ToListAsync();

        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product; // burada direkt ok çağrılabilir bu doğru ise return product 200 olarak bize dönecek
        }

        public async Task<Product> CreateProductAsync(Product product)

        {
            
                
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(int id,UpdateProductDto dto)

        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return false;

            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;
            existingProduct.Stock = dto.Stock;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)

        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return false;

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return true;
        }
        public decimal CalculateDiscount(decimal price, decimal rate)
        {
            decimal discount = price * rate / 100;
            decimal discountPrice = price - discount;
            return Math.Round(discountPrice, 2);
        }
        public decimal CalculateTax(decimal price, decimal taxRate)
        {
            decimal tax = price * taxRate / 100;
            return Math.Round(tax, 2);
        }

        public decimal CalculateTotal(decimal price, decimal discountRate, decimal taxRate)
        {
            var discountPrice = CalculateDiscount(price, discountRate);


            var tax = CalculateTax(discountPrice, taxRate);

            var total = discountPrice + tax;

            return Math.Round(total, 2);
        }

        public bool IsValid(Product product)

        {

            if (product.Price <= 0)
            { return false; }

            if (product.Stock < 0)
            { return false; }

            return true;


        }

        public bool HasEnoughStock(int productId, int requestedQuantity)
        {
            var control = _context.Products.FirstOrDefault(x=>x.Id == productId);
            if(control == null) return false;

            if(control.Stock<requestedQuantity ) return false; // Ürün stoğu istenen miktardan az ise 

            return true;
        }

        public bool DecreaseStock (int productId, int quantity)
        {
            var existingStock= _context.Products.FirstOrDefault( x=>x.Id == productId);
            if(existingStock == null) return false;

            if(existingStock.Stock < quantity) return false;

            existingStock.Stock -= quantity;
            _context.SaveChanges();
            return true;
        }
    }

}

  

