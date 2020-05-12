using BludataTest.Models;

namespace BludataTest.Services
{
    public class CompanyValidator
    {
        public bool isValid(Company company)
        {
            if(company.TradingName.Length <2)
            {
                return false;
            }
            if(isCNPJValid(company.Document.ToString()))
            {
                return false;
            }
            //validar coisitchas here
            return false;
        }

        private bool isCNPJValid(string cnpj)
        {
            return false;
        }
    }

}