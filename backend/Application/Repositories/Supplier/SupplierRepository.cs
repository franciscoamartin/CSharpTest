using System;
using System.Collections.Generic;
using System.Linq;
using BludataTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BludataTest.Repositories
{
    public class SupplierRepository
        : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(BludataTestDbContext dbContext) : base(dbContext)
        {
        }

        public override Supplier GetById(Guid id)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.FirstOrDefault(sup => sup.Id == id);
        }

        public override List<Supplier> GetAll()
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(sup => sup.Active).ToList();
        }

        public List<Supplier> GetByName(string name)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier => supplier.Name.Contains(name)).ToList();
        }

        public List<Supplier> GetByNameAndCompany(string name, Guid companyId)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier => supplier.Name.Contains(name)
                && supplier.CompanyId == companyId).ToList();
        }

        public List<Supplier> GetByDocument(string document)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier =>
                supplier.Document.ToString() == document.ToString()).ToList();
        }

        public List<Supplier> GetByDocumentAndCompany(string documentNumber, Guid companyId)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier => supplier.Document.ToString() == documentNumber
                 && supplier.CompanyId == companyId).ToList();
        }

        public List<Supplier> GetByRegisterTime(DateTime registerTime)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier => supplier.RegisterTime.Date == registerTime.Date).ToList();
        }

        public List<Supplier> GetByRegisterTimeAndCompany(DateTime registerTime, Guid companyId)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier => supplier.RegisterTime.Date == registerTime.Date
             && supplier.CompanyId == companyId).ToList();
        }

        public List<Supplier> GetSuppliersByCompany(Guid companyId)
        {
            var queryWithIncludes = getQueryWithIncludes();
            return queryWithIncludes.Where(supplier => supplier.CompanyId == companyId).ToList();
        }

        private IQueryable<Supplier> getQueryWithIncludes()
        {
            return _dbContext.Suppliers.Include(sup => sup.Company).Include(sup => sup.Telephones).Where(sup => sup.Active && sup.Company.Active);
        }
    }
}