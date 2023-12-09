using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore.Models
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext(DbContextOptions<MyStoreContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
        }
        public DbSet<Product> Products { get; set; }
    }
}