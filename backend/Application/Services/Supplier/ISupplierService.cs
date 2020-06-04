using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ResponseModels;
using BludataTest.ValueObject;

namespace BludataTest.Services
{
    public interface ISupplierService
    {
        void Create(Supplier supplier);
        IEnumerable<SupplierResponseModel> GetAll();
        Supplier Read(Guid id);
        List<Supplier> FindSuppliersByCompany(Guid companyId);
        IEnumerable<Supplier> FindByName(string name);
        Supplier FindByDocument(Document document);
        IEnumerable<Supplier> FindByRegisterTime(DateTime registerTime);
        void Delete(Guid id);
        void Update(Guid id, Supplier supplier);
    }
}