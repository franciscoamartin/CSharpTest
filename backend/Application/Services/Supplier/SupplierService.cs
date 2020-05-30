using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly DocumentValidator _documentValidator;
        private readonly SupplierValidator _supplierValidator;

        public SupplierService(ISupplierRepository supplierRepo)
        {
            _supplierRepository = supplierRepo;
            _documentValidator = new DocumentValidator();
            _supplierValidator = new SupplierValidator();
        }

        public void Create(Supplier supplier)
        {
            _supplierValidator.Validate(supplier);
            supplier.RegisterTime = DateTime.Now;
            _supplierRepository.Create(supplier);
        }
        public IEnumerable<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
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
                throw new Exception();
            var supplier = _supplierRepository.Read(id);
            if (supplier == null)
                throw new Exception("Fornecedor não encontrado");
            _supplierRepository.Delete(id);
        }
        public void Update(Guid id, Supplier supplier)
        {
            if (supplier.Id != id || id == Guid.Empty)
                throw new Exception();
            _supplierValidator.Validate(supplier);
            var _supplier = _supplierRepository.Read(id);
            if (_supplier == null)
                throw new Exception("Fornecedor não encontrado.");

            _supplier.Telephone = supplier.Telephone;
            _supplier.Name = supplier.Name;

            _supplierRepository.Update(_supplier);

        }

        public List<Supplier> FindSuppliersByCompany(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new Exception("A empresa precisa ser informada");
            return _supplierRepository.FindSuppliersByCompany(companyId);
        }
    }
}
