using System;
using BludataTest.ValueObject;


namespace BludataTest.Models
{
    public class Supplier
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

        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public Company Company { get; protected set; }
        public Document Document { get; protected set; }
        public DateTime RegisterTime {get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string Telephone { get; set; }
    }
}