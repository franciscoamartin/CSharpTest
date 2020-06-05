using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;

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
            _companyValidator.ValidateCompany(company);
            _companyRepository.Create(company);
        }
        public IEnumerable<Company> GetAll()
        {
            return _companyRepository.GetAll();

        }
        public Company Read(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("Informe a empresa");
            var company = _companyRepository.GetById(id);
            if (company == null)
                throw new Exception("Empresa não encontrada.");
            return company;
        }
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("Informe a empresa");
            var company = _companyRepository.GetById(id);
            if (company == null)
                throw new Exception("Empresa não encontrada.");
            _companyRepository.Delete(company);
        }
        public void Update(Guid id, Company company)
        {
            if (id == Guid.Empty || company.Id != id)
                throw new Exception("Informe uma empresa");
            var companyFound = _companyRepository.GetById(id);
            if (companyFound == null)
                throw new Exception("Empresa não encontrada.");

            companyFound.UF = company.UF;
            companyFound.TradingName = company.TradingName;

            _companyRepository.Update(companyFound);
        }
    }
}

