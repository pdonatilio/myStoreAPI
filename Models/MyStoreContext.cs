using Microsoft.EntityFrameworkCore;

namespace MyStoreApi.Models
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options)
        {}

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
    }
}