using BludataTest.Models;

namespace BludataTest.Repositories
{
    public class SupplierRepository 
        :GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(BludataTestDbContext dbContext) : base(dbContext)
        {

        }    
    }
}