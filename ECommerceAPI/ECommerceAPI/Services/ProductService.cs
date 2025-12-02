using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ECommerceAPI.Services
{
    public class ProductService
    {
        private readonly ECommerceContext _context;

        public ProductService(ECommerceContext context)
        {
            _context = context;
        }    


        public async Task<List<Product>> GetAllProductAsync ()
        {
           return await _context.Products.ToListAsync();
        }
    }
}
