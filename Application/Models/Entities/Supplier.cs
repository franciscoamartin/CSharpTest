using System;
using BludataTest.Repositories;
using BludataTest.ValueObject;


namespace BludataTest.Models
{
    public class Supplier : BaseEntity
        {
        public Supplier(string name, Company company, Document document, DateTime registerTime, DateTime birthDate, string telephone)
        {
            Name = name;
            Company = company;
            Document = document;
            RegisterTime = registerTime;
            BirthDate = birthDate;
            Telephone = telephone;
        }

        public string Name { get; set; }
        public Company Company { get; set; }
        public Document Document { get; set; }
        public DateTime RegisterTime {get; set; }
        public DateTime BirthDate { get; set; }
        public string Telephone { get; set; }
    }
}