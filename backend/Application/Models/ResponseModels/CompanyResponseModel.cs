namespace BludataTest.ResponseModels
{
    public class CompanyResponseModel
    {
        public CompanyResponseModel(string uF, string tradingName, string cnpj)
        {
            UF = uF;
            TradingName = tradingName;
            CNPJ = cnpj;
        }

        public string UF { get; set; }
        public string TradingName { get; set; }
        public string CNPJ { get; set; }
    }
}