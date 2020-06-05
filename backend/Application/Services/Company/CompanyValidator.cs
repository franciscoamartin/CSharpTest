using System;
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
                throw new Exception("Empresa não informada!");
            ValidateTradingName(company.TradingName);
            ValidateUF(company.UF);
            ValidateCNPJ(company.CNPJ);
        }
        private void ValidateTradingName(string tradingName)
        {
            if (string.IsNullOrWhiteSpace(tradingName) || tradingName.Length < 3)
                throw new Exception("Informe o nome fantasia corretamente"); ;
        }

        private void ValidateUF(string uf)
        {
            if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                throw new Exception("O estado não é válido.");
        }

        private void ValidateCNPJ(string cnpj)
        {
            if (!_documentValidator.isCNPJValid(cnpj))
                throw new Exception("Informe um CNPJ válido");
        }

    }
}