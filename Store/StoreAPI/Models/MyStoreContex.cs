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

        public DbSet<Product> Products { get; set; }
    }
}