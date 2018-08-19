using Microsoft.EntityFrameworkCore;

namespace MyStoreApi.Models
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options)
        {}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}