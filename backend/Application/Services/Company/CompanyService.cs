using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using BludataTest.CustomExceptions;

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
                throw new ValidationException("Informe a empresa");
            var company = _companyRepository.GetById(id);
            if (company == null)
                throw new ValidationException("Empresa não encontrada.");
            return company;
        }
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Informe a empresa");
            var company = _companyRepository.GetById(id);
            if (company == null)
                throw new ValidationException("Empresa não encontrada.");
            _companyRepository.Delete(company);
        }
        public void Update(Guid id, Company company)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Informe uma empresa");
            _companyValidator.ValidateUF(company.UF);
            _companyValidator.ValidateTradingName(company.TradingName);
            var companyToUpdate = _companyRepository.GetById(id);
            if (companyToUpdate == null)
                throw new ValidationException("Empresa não encontrada.");

            companyToUpdate.Update(company);

            _companyRepository.Update(companyToUpdate);
        }
    }
}

