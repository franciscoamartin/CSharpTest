using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BludataTest.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepo)
        {
            _supplierRepository = supplierRepo;
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
