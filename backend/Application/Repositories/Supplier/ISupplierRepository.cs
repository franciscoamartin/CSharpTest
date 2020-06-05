
using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        List<Supplier> FindSuppliersByCompany(Guid companyId);
        List<Supplier> FindByName(string name);
        List<Supplier> FindByNameAndCompany(string name, Guid companyId);
        List<Supplier> FindByDocument(string document);
        List<Supplier> FindByDocumentAndCompany(string document, Guid companyId);
        List<Supplier> FindByRegisterTime(DateTime registerTime);
        List<Supplier> FindByRegisterTimeAndCompany(DateTime registerTime, Guid companyId);
    }
}