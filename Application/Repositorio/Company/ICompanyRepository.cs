using System;
using System.Collections.Generic;
using BludataTest.Models;

namespace BludataTest.Repositorio
{
    public interface ICompanyRepository
    {
        void Create(Company company);
        IEnumerable<Company> GetAll();
        Company Read(Guid id);
        void Delete(Guid id);
        void Update(Company company);

    }
}