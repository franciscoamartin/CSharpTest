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
        public SupplierService(ISupplierRepository supplierRepo)
        {
            _supplierRepository = supplierRepo;
          _documentValidator = new DocumentValidator();
        }

        public void Create(Supplier supplier)
        {
            if(supplier==null) {
              throw new Exception();
            }            
            _supplierRepository.Create(supplier);
        }
         public IEnumerable<Supplier> GetAll()
        {
           return _supplierRepository.GetAll();
        }
        public Supplier Read(Guid id)
        {
            if(id == Guid.Empty)
                throw new Exception();
            var supplier = _supplierRepository.Read(id);
            if(supplier==null) 
                throw new Exception();
            return supplier;
        }
        public IEnumerable<Supplier> FindByName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new Exception();
            var suppliersFound = _supplierRepository.FindByName(name);
            if(suppliersFound==null) 
                throw new Exception();
            return suppliersFound;
        }
        public Supplier FindByDocument(Document document)
        {
            if(!_documentValidator.isValid(document))
                throw new Exception();
            var supplier = _supplierRepository.FindByDocument(document);
            if(supplier==null) 
                throw new Exception();
            return supplier;
        }
        public IEnumerable<Supplier> FindByRegisterTime(DateTime registerTime)
        {
            if(registerTime == null)
                throw new Exception();
            var supplier = _supplierRepository.FindByRegisterTime(registerTime);
            if(supplier==null) 
                throw new Exception();
            return supplier;
        }
        public void Delete(Guid id)
        {
            if(id == Guid.Empty)
                throw new Exception();
            var supplier = _supplierRepository.Read(id);
            if(supplier==null)
                throw new Exception();
            _supplierRepository.Delete(id);
        }
        public void Update(Guid id, Supplier supplier)
        {
            //REFATORAR ESSA CARALHA 
           if(supplier==null || supplier.Id != id || id == Guid.Empty) 
            {
              throw new Exception();
            }
            var _supplier = _supplierRepository.Read(id);

            if(_supplier==null) {
                throw new Exception();
            }

            _supplier.Telephone = supplier.Telephone;
            _supplier.Name = supplier.Name;

            _supplierRepository.Update(_supplier);
              
        }
    }
}
