using System;
using System.Collections.Generic;
using BludataTest.Repositorio;
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
            var supplier = _supplierRepository.Read(id);
            if(supplier==null) {
                throw new Exception();
            }
            return supplier;
        }
        public void Delete(Guid id)
        {
            var supplier = _supplierRepository.Read(id);

            if(supplier==null)
            {
                throw new Exception();
            }
            _supplierRepository.Delete(id);
        }
        public void Update(Guid id, Supplier supplier)
        {
           if(supplier==null || supplier.Id != id) 
            {
              throw new Exception();
            }
            var _supplier = _supplierRepository.Read(id);

            if(_supplier==null) {
                throw new Exception();
            }

            _supplier.Telefone = supplier.Telefone;
            _supplier.Nome = supplier.Nome;

            _supplierRepository.Update(_supplier);
              
        }
    }
}
