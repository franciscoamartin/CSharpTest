
using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {  
        IEnumerable<Supplier> FindByName(string name);
        Supplier FindByDocument(Document document);
        IEnumerable<Supplier> FindByRegisterTime(DateTime registerTime);
    }
}