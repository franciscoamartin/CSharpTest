using System;
using System.Collections.Generic;
using BludataTest.Models;

namespace BludataTest.Services
{
    public interface ISupplierService 
    {
        void Create(Supplier supplier);
        IEnumerable<Supplier> GetAll();
        Supplier Read(Guid id);
        void Delete(Guid id);
        void Update(Guid id, Supplier supplier);
    }
}