using Microsoft.EntityFrameworkCore;

namespace BludataTest.Models
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        { }

       public DbSet<Company> Companies { get; set; }
    }

}