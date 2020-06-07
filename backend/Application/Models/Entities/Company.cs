namespace BludataTest.Models
{
    public class Company : BaseEntity
    {
        public Company(string uF, string tradingName, string cNPJ)
        {
            UF = uF.ToUpper();
            TradingName = tradingName;
            CNPJ = cNPJ;
        }
        public Company()
        { }

        public string UF { get; set; }
        public string TradingName { get; set; }
        public string CNPJ { get; set; }

        public void Update(Company company)
        {
            UF = company.UF;
            TradingName = company.TradingName;
        }
    }
}