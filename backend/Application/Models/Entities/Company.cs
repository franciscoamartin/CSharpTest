namespace BludataTest.Models
{
    public class Company : BaseEntity
    {
        public Company(string uF, string tradingName, string cNPJ)
        {
            Active = true;
            UF = uF;
            TradingName = tradingName;
            CNPJ = cNPJ;
        }
        public Company()
        { }

        public string UF { get; set; }
        public string TradingName { get; set; }
        public string CNPJ { get; set; }
    }
}