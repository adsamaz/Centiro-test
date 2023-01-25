using Microsoft.EntityFrameworkCore;


namespace CentiroHomeAssignment.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {}

        public virtual DbSet<OrderModel> Orders { get; set; } = null;
        public virtual DbSet<ProductModel> Products { get; set; } = null;
    }
}