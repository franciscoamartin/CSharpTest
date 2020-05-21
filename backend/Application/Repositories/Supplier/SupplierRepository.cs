using System;
using System.Collections.Generic;
using System.Linq;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.Repositories
{
    public class SupplierRepository 
        :GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(BludataTestDbContext dbContext) : base(dbContext)
        {
        } 

        public IEnumerable<Supplier> FindByName(string name)
        {
            return _dbContext.Suppliers.Where(supplier => supplier.Name == name);
        }
        public Supplier FindByDocument(Document document)
        {
            return _dbContext.Suppliers.FirstOrDefault(supplier => supplier.Document.ToString()  == document.ToString());
        }
         public IEnumerable<Supplier> FindByRegisterTime(DateTime registerTime)
        {
            return _dbContext.Suppliers.Where(supplier => supplier.RegisterTime == registerTime);
        }

        public List<Supplier> FindSuppliersByCompany(Guid companyId)
        {
            return _dbContext.Suppliers.Where(supplier => supplier.CompanyId == companyId).ToList();
        }
    }
}