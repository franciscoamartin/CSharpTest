using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BludataTest.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private CompanyValidator _companyValidator;
        public CompanyService(ICompanyRepository companyRepo)
        {
            _companyRepository = companyRepo;
            _companyValidator = new CompanyValidator();
        }
        public void Create(Company company)
        {
            if(!_companyValidator.isValid(company)) {
              throw new Exception();
            }            
            _companyRepository.Create(company);
        }
         public IEnumerable<Company> GetAll()
        {
           return _companyRepository.GetAll();
        }
        public Company Read(Guid id)
        {
            if(id == Guid.Empty)
                throw new Exception();
            var company = _companyRepository.Read(id);
            if(company==null) 
               throw new Exception();
            return company;
        }
        public void Delete(Guid id)
        {
            if(id == Guid.Empty)
                throw new Exception();
            var company = _companyRepository.Read(id);
            if(!_companyValidator.isValid(company))
                throw new Exception();
            _companyRepository.Delete(id);
        }
        public void Update(Guid id, Company company)
        {
           if(!_companyValidator.isValid(company) || company.Id != id || id == Guid.Empty) 
            {
              throw new Exception();
            }
            var _company = _companyRepository.Read(id);

            if(_company==null) {
                throw new Exception();
            }

            _company.UF = company.UF;
            _company.TradingName = company.TradingName;

            _companyRepository.Update(_company);
        }
    }
}

