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

        public void Create(Supplier supplier)
        {
            var companyFound = _companyService.Read(supplier.CompanyId);
            supplier.Company = companyFound;


            supplier.RegisterTime = DateTime.Now;
            _supplierValidator.Validate(supplier);
            _supplierRepository.Create(supplier);
        }
        public IEnumerable<SupplierResponseModel> GetAll()
        {

            var suppliersFound = _supplierRepository.GetAll().ToList();
            var suppliersToReturn = suppliersFound.Select(s => new SupplierResponseModel(id: s.Id, name: s.Name, companyTradingName: s.Company.TradingName, document: s.Document, rg: s.RG, registerTime: s.RegisterTime, birthDate: s.BirthDate, telephone: s.Telephones));
            return suppliersToReturn;
        }
        public Supplier Read(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("Fornecedor precisa ser informado");
            var supplier = _supplierRepository.Read(id);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            return supplier;
        }
        public IEnumerable<Supplier> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("O nome precisa ser informado");
            var suppliersFound = _supplierRepository.FindByName(name);
            if (suppliersFound == null)
                throw new Exception("Fornecedor não encontrado");
            return suppliersFound;
        }
        public Supplier FindByDocument(Document document)
        {
            if (!_documentValidator.isValid(document))
                throw new Exception("O documento informado não é válido");
            var supplier = _supplierRepository.FindByDocument(document);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            return supplier;
        }
        public IEnumerable<Supplier> FindByRegisterTime(DateTime registerTime)
        {
            if (registerTime == null)
                throw new Exception("Data/Hora de cadastro precisam ser informados");
            var supplier = _supplierRepository.FindByRegisterTime(registerTime);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            return supplier;
        }
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("O fornecedor precisa ser informado.");
            var supplier = _supplierRepository.Read(id);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            _supplierRepository.Delete(supplier);
        }
        public void Update(Guid id, Supplier supplier)
        {
            if (supplier.Id != id || id == Guid.Empty)
                throw new Exception("Fornecedor precisa ser informado.");
            _supplierValidator.ValidateTelephones(supplier.Telephones);
            _supplierValidator.ValidateName(supplier.Name);

            var supplierFound = _supplierRepository.Read(id);
            if (supplierFound == null)
                throw new Exception("Fornecedor não encontrado.");

            supplierFound.Telephones = supplier.Telephones;
            supplierFound.Name = supplier.Name;

            _supplierRepository.Update(supplierFound);
        }

        public List<Supplier> FindSuppliersByCompany(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new Exception("A empresa precisa ser informada");
            return _supplierRepository.FindSuppliersByCompany(companyId);
        }
    }
}
