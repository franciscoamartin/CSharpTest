using System;

namespace BludataTest.Models
{
    public class Company
    {
        public Guid CompanyID { get; set; }
        public string UF { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
    }
}