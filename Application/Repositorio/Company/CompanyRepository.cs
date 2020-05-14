using System;
using System.Collections.Generic;
using System.Linq;
using BludataTest.Models;

namespace BludataTest.Repositorio
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly BludataTestDbContext _contexto;
        public CompanyRepository(BludataTestDbContext ctx)
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
            return _contexto.Companies.FirstOrDefault(e => e.Id == id);
        }

        public void Delete(Guid id)
        {
            var entity =_contexto.Companies.First(e => e.Id == id);
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