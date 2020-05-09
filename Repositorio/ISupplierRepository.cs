using System;
using System.Collections.Generic;
using BludataTest.Models;

namespace BludataTest.Repositorio
{
    public interface ISupplierRepository
    {
        void Create(Supplier supplier);
        IEnumerable<Supplier> GetAll();
        Supplier Read(Guid id);
        void Delete(Guid id);
        void Update(Supplier supplier);

    }
}