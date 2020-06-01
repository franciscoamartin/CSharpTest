using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.ResponseModels
{
    public class SupplierResponseModel : Supplier
    {
        public SupplierResponseModel(string name, Company company, Guid companyId, Document document, string rg, DateTime registerTime, DateTime? birthDate, List<Telephones> telephone) : base(name, company, companyId, document, rg, registerTime, birthDate, telephone)
        {
            CpfCnpj = document.ToString();
        }
        public string CpfCnpj { get; set; }
    }

}