using System;
using System.Collections.Generic;
using System.Linq;
using BludataTest.Models;

namespace BludataTest.Repositorio
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyDbContext _contexto;
        public CompanyRepository(CompanyDbContext ctx)
        {
            _contexto = ctx;
        }
        public void Create(Company company)
        {
            _contexto.Companies.Add(company);
            _contexto.SaveChanges();
        }
         public IEnumerable<Company> GetAll()
        {
           return _contexto.Companies.ToList();
        }

        public Company Read(Guid id)
        {
            return _contexto.Companies.FirstOrDefault(e => e.CompanyID == id);
        }

        public void Delete(Guid id)
        {
            var entity =_contexto.Companies.First(e => e.CompanyID == id);
            _contexto.Companies.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(Company company)
        {
            _contexto.Companies.Update(company);
            _contexto.SaveChanges();
        }
    }
}