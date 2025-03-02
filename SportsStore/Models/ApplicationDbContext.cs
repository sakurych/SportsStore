using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { Database.EnsureCreated(); }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Ordres { get; set; }
    }
}
