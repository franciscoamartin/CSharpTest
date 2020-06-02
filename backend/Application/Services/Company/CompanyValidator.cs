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
        public bool isValid(Company company)
        {
            if (company == null)
                throw new Exception("Empresa não informada!"); ;
            if (string.IsNullOrWhiteSpace(company.TradingName) || company.TradingName.Length < 3)
                throw new Exception("Informe o nome fantasia corretamente"); ;
            if (string.IsNullOrWhiteSpace(company.UF) || company.UF.Length != 2)
                throw new Exception("O estado não é válido."); ;
            if (!_documentValidator.isCNPJValid(company.CNPJ))
                throw new Exception("Informe um CNPJ válido"); ;
            return true;
        }


    }
}