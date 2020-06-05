using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using BludataTest.ValueObject;
using System.Linq;
using BludataTest.ResponseModels;

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

            var suppliersFound = _supplierRepository.GetAll();
            var suppliersToReturn = getSuppliersResponseModels(suppliersFound);
            return suppliersToReturn;
        }

        private List<SupplierResponseModel> getSuppliersResponseModels(List<Supplier> suppliersFound)
        {
            return suppliersFound.Select(s => new SupplierResponseModel(id: s.Id,
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

        public List<SupplierResponseModel> FindSuppliersByCompany(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new Exception("A empresa precisa ser informada");
            var suppliersFound = _supplierRepository.FindSuppliersByCompany(companyId);
            validateSuppliersSearch(suppliersFound);
            return getSuppliersResponseModels(suppliersFound);
        }

        public void Create(Supplier supplier)
        {
            var companyFound = _companyService.Read(supplier.CompanyId);
            supplier.Company = companyFound;


            supplier.RegisterTime = DateTime.Now;
            _supplierValidator.Validate(supplier);
            _supplierRepository.Create(supplier);
        }

        public void Update(Guid id, Supplier supplier)
        {
            if (supplier.Id != id || id == Guid.Empty)
                throw new Exception("Fornecedor precisa ser informado.");
            _supplierValidator.ValidateTelephones(supplier.Telephones);
            _supplierValidator.ValidateName(supplier.Name);

            var supplierFound = _supplierRepository.GetById(id);
            if (supplierFound == null)
                throw new Exception("Fornecedor não encontrado.");

            supplierFound.Telephones = supplier.Telephones;
            supplierFound.Name = supplier.Name;

            _supplierRepository.Update(supplierFound);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("O fornecedor precisa ser informado.");
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            _supplierRepository.Delete(supplier);
        }

        public List<SupplierResponseModel> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("O nome precisa ser informado");
            var suppliersFound = _supplierRepository.FindByName(name);
            validateSuppliersSearch(suppliersFound);
            return getSuppliersResponseModels(suppliersFound);
        }
        public List<SupplierResponseModel> FindByNameAndCompany(string name, Guid companyId)
        {
            if (string.IsNullOrWhiteSpace(name) || companyId == Guid.Empty)
                throw new Exception("O nome e a empresa precisam ser informados");
            var suppliersFound = _supplierRepository.FindByNameAndCompany(name, companyId);
            validateSuppliersSearch(suppliersFound);
            return getSuppliersResponseModels(suppliersFound);
        }

        public List<SupplierResponseModel> FindByDocument(string document)
        {
            if (!_documentValidator.isCNPJValid(document) && !_documentValidator.isCPFValid(document))
                throw new Exception("O documento informado não é válido");
            var suppliersFound = _supplierRepository.FindByDocument(document);
            validateSuppliersSearch(suppliersFound);
            return getSuppliersResponseModels(suppliersFound);

        }
        public List<SupplierResponseModel> FindByDocumentAndCompany(string document, Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new Exception("A empresa precisa ser informada.");
            if (!_documentValidator.isCNPJValid(document) && !_documentValidator.isCPFValid(document))
                throw new Exception("O documento informado não é válido");
            var suppliersFound = _supplierRepository.FindByDocumentAndCompany(document, companyId);
            if (suppliersFound == null)
                throw new Exception("Fornecedor não encontrado");
            return getSuppliersResponseModels(suppliersFound);

        }

        public List<SupplierResponseModel> FindByRegisterTime(string registerTime)
        {
            if (string.IsNullOrWhiteSpace(registerTime))
                throw new Exception("Data/Hora de cadastro precisam ser informados");
            var dateToSearch = getFormattedDate(registerTime);
            var suppliersFound = _supplierRepository.FindByRegisterTime(dateToSearch);
            validateSuppliersSearch(suppliersFound);
            return getSuppliersResponseModels(suppliersFound);
        }
        public List<SupplierResponseModel> FindByRegisterTimeAndCompany(string registerTime, Guid companyId)
        {
            if (string.IsNullOrWhiteSpace(registerTime) || companyId == Guid.Empty)
                throw new Exception("Data/Hora de cadastro e empresa precisam ser informados");
            var dateToSearch = getFormattedDate(registerTime);
            var suppliersFound = _supplierRepository.FindByRegisterTimeAndCompany(dateToSearch, companyId);
            validateSuppliersSearch(suppliersFound);
            return getSuppliersResponseModels(suppliersFound);
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
                throw new Exception("Data de cadastro inválida");
            }
        }

        private void validateSuppliersSearch(List<Supplier> suppliers)
        {
            if (suppliers.Count <= 0)
                throw new Exception("Fornecedor não encontrado");
        }

    }
}
