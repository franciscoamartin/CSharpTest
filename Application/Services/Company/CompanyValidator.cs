using BludataTest.Enums;
using BludataTest.Models;
using Validacao;

namespace BludataTest.Services
{
    public class CompanyValidator : CNPJValidator
    {
        public bool isValid(Company company)
        {
			if(company == null)
			  return false;			   
            if(string.IsNullOrWhiteSpace(company.TradingName) || company.TradingName.Length < 3)
              return false;
            if(string.IsNullOrWhiteSpace(company.UF) || company.UF.Length != 2)
              return false;
            if(company.Document.Type != EDocumentType.CNPJ || !isCNPJValid(company.Document.ToString()))
              return false;
            return true;
        }

        
    }
}