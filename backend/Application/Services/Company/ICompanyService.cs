using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ResponseModels;

namespace BludataTest.Services
{
    public interface ICompanyService
    {
        void Create(Company company);
        IEnumerable<CompanyResponseModel> GetAll();
        CompanyResponseModel Read(Guid id);
        void Delete(Guid id);
        void Update(Guid id, Company company);

    }
}