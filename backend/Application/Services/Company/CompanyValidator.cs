using BludataTest.Enums;
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
			      if(company == null)
        	   return false;			   
            if(string.IsNullOrWhiteSpace(company.TradingName) || company.TradingName.Length < 3)
              return false;
            if(string.IsNullOrWhiteSpace(company.UF) || company.UF.Length != 2)
              return false;
            if(company.Document.Type != EDocumentType.CNPJ || _documentValidator.isValid(company.Document))
              return false;
            return true;
        }

        
    }
}