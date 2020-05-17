using System;
using BludataTest.Repositories;
using BludataTest.ValueObject;

namespace BludataTest.Models
{
    public class Company : BaseEntity
    {
        public Company(string uf, string tradingName, Document document)
        {
            UF = uf;
            TradingName = tradingName;
            Document = document;
        }

       
        public string UF { get; set; }
        public string TradingName { get; set; }
        public Document Document { get; set; }



    }
}