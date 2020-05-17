using BludataTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BludataTest.Models
{
    public class BludataTestDbContext : DbContext
    {
        public BludataTestDbContext(DbContextOptions<BludataTestDbContext> options)
            : base(options)
        { }

       public DbSet<Company> Companies { get; set; }
       public DbSet<Supplier> Suppliers { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
          modelBuilder.ApplyConfiguration(new CompanyMapping());
          modelBuilder.ApplyConfiguration(new SupplierMapping());
       }
    }
}