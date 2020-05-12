using System;
using BludataTest.ValueObject;

namespace BludataTest.Models
{
    public class Company
    {
        public Guid Id { get; protected set; }
        public string UF { get; set; }
        public string TradingName { get; set; }
        public Document Document { get; protected set; }
    }
}