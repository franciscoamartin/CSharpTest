using BludataTest.Models;

namespace BludataTest.Repositories
{
    public class CompanyRepository
        : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(BludataTestDbContext dbContext) : base(dbContext)
        {
        }
    }
}