using System;
using System.Collections.Generic;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.ResponseModels
{
    public class SupplierResponseModel
    {
        public SupplierResponseModel(Guid id, string name, Document document, string companyTradingName,
        string rg, DateTime registerTime, DateTime? birthDate, List<Telephone> telephone)
        {
            Id = id;
            Name = name;
            RG = rg;
            CpfCnpj = document.ToString();
            CompanyTradingName = companyTradingName;
            BirthDate = birthDate?.ToString("dd/MM/yyyy");
            RegisterTime = registerTime.ToString("dd/MM/yyyy HH:mm:ss");
            Telephones = telephone.ConvertAll(tel => tel.Number);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string RG { get; set; }
        public List<string> Telephones { get; set; }
        public string CpfCnpj { get; set; }
        public string CompanyTradingName { get; set; }
        public string RegisterTime { get; set; }
    }

}