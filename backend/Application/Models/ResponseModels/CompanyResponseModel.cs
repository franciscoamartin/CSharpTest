using System;

namespace BludataTest.ResponseModels
{
    public class CompanyResponseModel
    {
        public CompanyResponseModel(Guid companyId, string uF, string tradingName, string cnpj)
        {
            CompanyId = companyId;
            UF = uF;
            TradingName = tradingName;
            CNPJ = cnpj;
        }

        public Guid CompanyId { get; set; }
        public string UF { get; set; }
        public string TradingName { get; set; }
        public string CNPJ { get; set; }
    }
}