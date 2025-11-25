using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Data
{
    public class ECommerceContext :DbContext{
        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

       
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
