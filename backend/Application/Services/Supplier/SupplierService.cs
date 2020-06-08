using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using System.Linq;
using BludataTest.ResponseModels;
using BludataTest.CustomExceptions;

namespace BludataTest.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly DocumentValidator _documentValidator;
        private readonly SupplierValidator _supplierValidator;
        private readonly ICompanyService _companyService;

        public SupplierService(ISupplierRepository supplierRepo, ICompanyService companyService)
        {
            _companyService = companyService;
            _supplierRepository = supplierRepo;
            _documentValidator = new DocumentValidator();
            _supplierValidator = new SupplierValidator();
        }

        public List<SupplierResponseModel> GetAll()
        {
            var foundSuppliers = _supplierRepository.GetAll();
            var suppliersToReturn = getSuppliersResponseModels(foundSuppliers);
            return suppliersToReturn;
        }

        private List<SupplierResponseModel> getSuppliersResponseModels(List<Supplier> foundSuppliers)
        {
            return foundSuppliers.Select(s => new SupplierResponseModel(id: s.Id,
            name: s.Name, companyTradingName: s.Company.TradingName, document: s.Document,
            rg: s.RG, registerTime: s.RegisterTime, birthDate: s.BirthDate, telephone: s.Telephones)).ToList();
        }

        public Supplier GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("Fornecedor precisa ser informado");
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            return supplier;
        }

        public List<SupplierResponseModel> GetSuppliersByCompany(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new Exception("A empresa precisa ser informada");
            var foundSuppliers = _supplierRepository.GetSuppliersByCompany(companyId);
            validateSuppliersSearch(foundSuppliers);
            return getSuppliersResponseModels(foundSuppliers);
        }

        public void Create(Supplier supplier)
        {
            if (supplier == null)
                throw new Exception("Dados informados não estão corretos!");
            var companyFound = _companyService.Read(supplier.CompanyId);
            supplier.Company = companyFound;


            supplier.RegisterTime = DateTime.Now;
            _supplierValidator.ValidateSupplier(supplier);
            _supplierRepository.Create(supplier);
        }

        public void Update(Guid id, Supplier supplier)
        {
            if (supplier == null)
                throw new ValidationException("Dados informados não estão corretos!");
            if (id == Guid.Empty)
                throw new ValidationException("Fornecedor precisa ser informado.");
            _supplierValidator.ValidateTelephones(supplier.Telephones);
            _supplierValidator.ValidateName(supplier.Name);

            var supplierToUpdate = _supplierRepository.GetById(id);
            if (supplierToUpdate == null)
                throw new ValidationException("Fornecedor não encontrado.");

            supplierToUpdate.Update(supplier);
            _supplierRepository.Update(supplierToUpdate);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("O fornecedor precisa ser informado.");
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null)
                throw new ValidationException("Fornecedor não encontrado");
            _supplierRepository.Delete(supplier);
        }

        public List<SupplierResponseModel> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("O nome precisa ser informado");
            var foundSuppliers = _supplierRepository.GetByName(name);
            validateSuppliersSearch(foundSuppliers);
            return getSuppliersResponseModels(foundSuppliers);
        }
        public List<SupplierResponseModel> GetByNameAndCompany(string name, Guid companyId)
        {
            if (string.IsNullOrWhiteSpace(name) || companyId == Guid.Empty)
                throw new ValidationException("O nome e a empresa precisam ser informados");
            var foundSuppliers = _supplierRepository.GetByNameAndCompany(name, companyId);
            validateSuppliersSearch(foundSuppliers);
            return getSuppliersResponseModels(foundSuppliers);
        }

        public List<SupplierResponseModel> GetByDocument(string document)
        {
            if (!_documentValidator.isCNPJValid(document) && !_documentValidator.isCPFValid(document))
                throw new ValidationException("O documento informado não é válido");
            var foundSuppliers = _supplierRepository.GetByDocument(document);
            validateSuppliersSearch(foundSuppliers);
            return getSuppliersResponseModels(foundSuppliers);
        }
        public List<SupplierResponseModel> GetByDocumentAndCompany(string document, Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new ValidationException("A empresa precisa ser informada.");
            if (!_documentValidator.isCNPJValid(document) && !_documentValidator.isCPFValid(document))
                throw new ValidationException("O documento informado não é válido");
            var foundSuppliers = _supplierRepository.GetByDocumentAndCompany(document, companyId);
            if (foundSuppliers == null)
                throw new ValidationException("Fornecedor não encontrado");
            return getSuppliersResponseModels(foundSuppliers);

        }

        public List<SupplierResponseModel> GetByRegisterTime(string registerTime)
        {
            if (string.IsNullOrWhiteSpace(registerTime))
                throw new ValidationException("Data/Hora de cadastro precisam ser informados");
            var dateToSearch = getFormattedDate(registerTime);
            var foundSuppliers = _supplierRepository.GetByRegisterTime(dateToSearch);
            validateSuppliersSearch(foundSuppliers);
            return getSuppliersResponseModels(foundSuppliers);
        }
        public List<SupplierResponseModel> GetByRegisterTimeAndCompany(string registerTime, Guid companyId)
        {
            if (string.IsNullOrWhiteSpace(registerTime) || companyId == Guid.Empty)
                throw new ValidationException("Data/Hora de cadastro e empresa precisam ser informados");
            var dateToSearch = getFormattedDate(registerTime);
            var foundSuppliers = _supplierRepository.GetByRegisterTimeAndCompany(dateToSearch, companyId);
            validateSuppliersSearch(foundSuppliers);
            return getSuppliersResponseModels(foundSuppliers);
        }

        private DateTime getFormattedDate(string registerTime)
        {
            try
            {
                var yearMonthDay = registerTime.Split("-");
                var year = int.Parse(yearMonthDay[0]);
                var month = int.Parse(yearMonthDay[1]);
                var day = int.Parse(yearMonthDay[2]);
                var dateToSearch = new DateTime(year, month, day);

                return dateToSearch;
            }
            catch (Exception)
            {
                throw new ValidationException("Data de cadastro inválida");
            }
        }

        private void validateSuppliersSearch(List<Supplier> suppliers)
        {
            if (suppliers == null || suppliers.Count <= 0)
                throw new ValidationException("Fornecedor não encontrado");
        }
    }
}
