using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.Services
{
    public interface ISupplierService 
    {
        void Create(Supplier supplier);
        IEnumerable<Supplier> GetAll();
        Supplier Read(Guid id);
        IEnumerable<Supplier> FindByName(string name);
        Supplier FindByDocument(Document document);
        IEnumerable<Supplier> FindByRegisterTime(DateTime registerTime);
        void Delete(Guid id);
        void Update(Guid id, Supplier supplier);
        List<Supplier> FindSuppliersByCompany(Guid companyId);
    }
}