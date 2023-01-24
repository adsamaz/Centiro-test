using Microsoft.EntityFrameworkCore;

namespace CentiroHomeAssignment.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {}

        public DbSet<OrderModel> Orders { get; set; } = null;
        public DbSet<ProductModel> Products { get; set; } = null;
    }
}