using BludataTest.Models;
using BludataTest.ResponseModels;
using System;
using System.Collections.Generic;

namespace BludataTest.Services
{
    public interface ISupplierService
    {

        List<SupplierResponseModel> GetAll();
        Supplier GetById(Guid id);
        void Create(Supplier supplier);
        void Update(Guid id, Supplier supplier);
        void Delete(Guid id);
        List<SupplierResponseModel> GetSuppliersByCompany(Guid companyId);
        List<SupplierResponseModel> GetByName(string name);
        List<SupplierResponseModel> GetByDocument(string document);
        List<SupplierResponseModel> GetByRegisterTime(string registerTime);
        List<SupplierResponseModel> GetByNameAndCompany(string name, Guid companyId);
        List<SupplierResponseModel> GetByDocumentAndCompany(string document, Guid companyId);
        List<SupplierResponseModel> GetByRegisterTimeAndCompany(string registerTime, Guid companyId);
    }
}