using Microsoft.EntityFrameworkCore;


namespace CentiroHomeAssignment.Models
{
    public interface IDbContext
{
    DbSet<T> Set<T>() where T : class;
    int SaveChanges();
}
    public class OrderContext : DbContext, IDbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {}

        public virtual DbSet<OrderModel> Orders { get; set; } = null;
        public virtual DbSet<ProductModel> Products { get; set; } = null;
    }
}