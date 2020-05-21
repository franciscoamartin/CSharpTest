using System;
using BludataTest.ValueObject;

namespace BludataTest.Models
{
    public class Company : BaseEntity
    {
        public Company(string uF, string tradingName, Document document)
        {
            UF = uF;
            TradingName = tradingName;
            Document = document;
        }

        public string UF { get; set; }
        public string TradingName { get; set; }
        public Document Document { get; set; }
    }
}