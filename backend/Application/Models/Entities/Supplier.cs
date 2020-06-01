using System;
using System.Collections.Generic;
using BludataTest.ValueObject;


namespace BludataTest.Models
{
    public class Supplier : BaseEntity
    {
        public Supplier(string name, Company company, Guid companyId, Document document, string rg, DateTime registerTime, DateTime? birthDate, List<Telephones> telephone)
        {
            Name = name;
            Company = company;
            CompanyId = companyId;
            Document = document;
            RG = rg;
            RegisterTime = registerTime;
            BirthDate = birthDate;
            Telephone = telephone;
        }

        public Supplier()
        {
        }

        public string Name { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Document Document { get; set; }
        public string RG { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<Telephones> Telephone { get; set; }
    }
}