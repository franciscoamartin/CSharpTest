using System;
using System.Linq;
using BludataTest.CustomExceptions;
using BludataTest.Models;

namespace BludataTest.Services
{
    public class CompanyValidator
    {
        private readonly DocumentValidator _documentValidator;
        public CompanyValidator()
        {
            _documentValidator = new DocumentValidator();
        }
        public void ValidateCompany(Company company)
        {
            if (company == null)
                throw new ValidationException("Empresa não informada!");
            ValidateTradingName(company.TradingName);
            ValidateUF(company.UF);
            ValidateCNPJ(company.CNPJ);
        }
        public void ValidateTradingName(string tradingName)
        {
            if (string.IsNullOrWhiteSpace(tradingName) || tradingName.Length < 3)
                throw new ValidationException("Informe o nome fantasia corretamente"); ;
        }

        public void ValidateUF(string uf)
        {
            if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                throw new ValidationException("O estado não é válido.");
        }

        private void ValidateCNPJ(string cnpj)
        {
            if (!_documentValidator.isCNPJValid(cnpj))
                throw new ValidationException("Informe um CNPJ válido");
        }

        private bool UfContainsNumber(string uf)
        {
            return uf.Any(c => char.IsDigit(c));
        }

    }
}