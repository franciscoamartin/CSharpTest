using System;
using BludataTest.ValueObject;


namespace BludataTest.Models
{
    public class Supplier : BaseEntity
        {
        public Supplier(string name, Company company, Guid companyId, Document document, DateTime registerTime, DateTime birthDate, Telephone telephone)
        {
            Name = name;
            Company = company;
            CompanyId = companyId;
            Document = document;
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
        public Document Document { get; set; }
        public DateTime RegisterTime {get; set; }
        public DateTime BirthDate { get; set; }
        public Telephone Telephone { get; set; }
    }
}