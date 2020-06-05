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
        List<SupplierResponseModel> GetSuppliersByCompany(Guid companyId);
        List<SupplierResponseModel> GetByName(string name);
        List<SupplierResponseModel> GetByDocument(string document);
        List<SupplierResponseModel> GetByRegisterTime(string registerTime);
        void Delete(Guid id);
        void Update(Guid id, Supplier supplier);
        List<SupplierResponseModel> GetByNameAndCompany(string name, Guid companyId);
        List<SupplierResponseModel> GetByDocumentAndCompany(string document, Guid companyId);
        List<SupplierResponseModel> GetByRegisterTimeAndCompany(string registerTime, Guid companyId);
    }
}