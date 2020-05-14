using System;
using System.Collections.Generic;
using BludataTest.Models;

namespace BludataTest.Services
{
    public interface ICompanyService 
    {
        void Create(Company company);
        IEnumerable<Company> GetAll();
        Company Read(Guid id);
        void Delete(Guid id);
        void Update(Guid id, Company company);

    }
}