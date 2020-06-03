using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.ResponseModels
{
    public class SupplierResponseModel : Supplier
    {
        public SupplierResponseModel(Guid id, string name, Company company, Guid companyId, Document document,
        string rg, DateTime registerTime, DateTime? birthDate, List<Telephones> telephone)
            : base(name, company, companyId, document, rg, registerTime, birthDate, telephone)
        {
            Id = id;
            CpfCnpj = document.ToString();
            CompanyTradingName = company.TradingName;
            FormattedRegisterTime = registerTime.ToString("dd/MM/yyyy HH:mm:ss");
            FormattedTelephones = telephone.ConvertAll(tel => tel.Number);
        }

        public List<string> FormattedTelephones { get; set; }
        public string CpfCnpj { get; set; }
        public string CompanyTradingName { get; set; }
        public string FormattedRegisterTime { get; set; }
    }

}