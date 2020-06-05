using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ResponseModels;

namespace BludataTest.Services
{
    public interface ISupplierService
    {
        void Create(Supplier supplier);
        List<SupplierResponseModel> GetAll();
        Supplier GetById(Guid id);
        List<SupplierResponseModel> FindSuppliersByCompany(Guid companyId);
        List<SupplierResponseModel> FindByName(string name);
        List<SupplierResponseModel> FindByDocument(string document);
        List<SupplierResponseModel> FindByRegisterTime(string registerTime);
        void Delete(Guid id);
        void Update(Guid id, Supplier supplier);
        List<SupplierResponseModel> FindByNameAndCompany(string name, Guid companyId);
        List<SupplierResponseModel> FindByDocumentAndCompany(string document, Guid companyId);
        List<SupplierResponseModel> FindByRegisterTimeAndCompany(string registerTime, Guid companyId);
    }
}