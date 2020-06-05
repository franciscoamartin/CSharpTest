using System;
using System.Collections.Generic;
using BludataTest.Models;

namespace BludataTest.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        List<Supplier> GetSuppliersByCompany(Guid companyId);
        List<Supplier> GetByName(string name);
        List<Supplier> GetByNameAndCompany(string name, Guid companyId);
        List<Supplier> GetByDocument(string document);
        List<Supplier> GetByDocumentAndCompany(string document, Guid companyId);
        List<Supplier> GetByRegisterTime(DateTime registerTime);
        List<Supplier> GetByRegisterTimeAndCompany(DateTime registerTime, Guid companyId);
    }
}