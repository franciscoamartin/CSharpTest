using System;
using System.Collections.Generic;
using System.Linq;
using BludataTest.Models;

namespace BludataTest.Repositorio
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly BludataTestDbContext _contexto;
        public SupplierRepository(BludataTestDbContext ctx)
        {
            _contexto = ctx;
        }
        public void Create(Supplier supplier)
        {
            _contexto.Suppliers.Add(supplier);
            _contexto.SaveChanges();
        }
         public IEnumerable<Supplier> GetAll()
        {
           return _contexto.Suppliers.ToList();
        }

        public Supplier Read(Guid id)
        {
            return _contexto.Suppliers.FirstOrDefault(e => e.Id == id);
        }

        public void Delete(Guid id)
        {
            var entity =_contexto.Suppliers.First(e => e.Id == id);
            _contexto.Suppliers.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            _contexto.Suppliers.Update(supplier);
            _contexto.SaveChanges();
        }
    }
}