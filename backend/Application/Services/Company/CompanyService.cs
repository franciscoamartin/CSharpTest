using System;
using System.Collections.Generic;
using BludataTest.Repositories;
using BludataTest.Models;
using System.Linq;
using BludataTest.ResponseModels;

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
            if (!_companyValidator.isValid(company))
            {
                throw new Exception();
            }
            _companyRepository.Create(company);
        }
        public IEnumerable<CompanyResponseModel> GetAll()
        {
            var companies = _companyRepository.GetAll();
            return companies.Select(company => new CompanyResponseModel(
                uF: company.UF,
                tradingName: company.TradingName,
                cnpj: company.Document.ToString()));
        }
        public CompanyResponseModel Read(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception();
            var company = _companyRepository.Read(id);
            if (company == null)
                throw new Exception("Empresa não encontrada.");
            return new CompanyResponseModel(
                uF: company.UF,
                tradingName: company.TradingName,
                cnpj: company.Document.ToString());
        }
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception();
            var company = _companyRepository.Read(id);
            if (company == null)
                throw new Exception("Empresa não encontrada.");
            _companyRepository.Delete(id);
        }
        public void Update(Guid id, Company company)
        {
            if (id == Guid.Empty || company.Id != id || !_companyValidator.isValid(company))
                throw new Exception();
            var _company = _companyRepository.Read(id);
            if (_company == null)
                throw new Exception("Empresa não encontrada.");

            _company.UF = company.UF;
            _company.TradingName = company.TradingName;

            _companyRepository.Update(_company);
        }
    }
}

