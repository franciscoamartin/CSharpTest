using System;
using System.Collections.Generic;
using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.Repositories;
using BludataTest.Services;
using BludataTest.ValueObject;
using NSubstitute;
using Xunit;

namespace UnitTests.ServicesTests
{
    public class SupplierServicesTests
    {
        private ISupplierService _supplierService;
        private ISupplierRepository _supplierRepository;
        public SupplierServicesTests()
        {
            _supplierRepository = Substitute.For<ISupplierRepository>();
            _supplierService = new SupplierService(_supplierRepository);
        }
        private Company GetCompanyExample()
        {
            var company = new Company(uF: "SC", tradingName: "Mercado Chicão", cNPJ: "68.356.468/0001-57");
            return company;
        }

        private List<Telephones> GetTelephoneExample()
        {
            var telephones = new List<Telephones>();
            telephones.Add(new Telephones("4733375582"));
            telephones.Add(new Telephones("4733784158"));
            telephones.Add(new Telephones("47992186559"));
            return telephones;
        }

        private Supplier GetSupplierExample()
        {
            var company = GetCompanyExample();
            var telephones = GetTelephoneExample();
            var supplier = new Supplier(name: "Ronaldo", company: company, companyId: company.Id, document: new Document("086.263.709-03", EDocumentType.CPF), rg: "623267", registerTime: DateTime.Now, birthDate: new DateTime(2001, 12, 7), telephone: new List<Telephones>());
            return supplier;
        }

        [Fact]
        public void Should_create_supplier()
        {
            var supplier = GetSupplierExample();
            _supplierService.Create(supplier);
            _supplierRepository.Received(1).Create(Arg.Is<Supplier>(s =>
             s.Name == "Ronaldo" &&
             s.Company.ToString() == "Mercado Chicão" &&
             s.Document.ToString() == "086.263.709-03" &&
             s.RG == "623267" &&
             s.RegisterTime == DateTime.Now &&
             s.BirthDate == new DateTime(2001, 12, 7) &&
             s.Telephone.ToString() == "4733375582"));
        }

        [Fact]
        public void Should_not_create_supplier()
        {
            var supplier = GetSupplierExample();
            supplier.Name = "L";
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }

        [Fact]
        public void Should_not_create_supplier_when_birthDate_is_wrong()
        {
            var supplier = GetSupplierExample();
            supplier.BirthDate = DateTime.Now.AddDays(1);
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }

        [Fact]
        public void Should_not_create_supplier_when_birthDate_is_before_1900()
        {
            var supplier = GetSupplierExample();
            supplier.BirthDate = new DateTime(1899, 12, 31);
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }

        [Fact]
        public void Should_not_create_supplier_when_CPF_is_wrong()
        {
            var supplier = GetSupplierExample();
            supplier.Document = new Document("083.27.709-03", EDocumentType.CPF);
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }

        [Fact]
        public void Should_not_create_supplier_when_CNPJ_is_wrong()
        {
            var supplier = GetSupplierExample();
            supplier.Document = new Document("18.321.410/001-42", EDocumentType.CNPJ);
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }

        [Fact]
        public void Should_not_create_supplier_when_telephone_is_wrong()
        {
            var supplier = GetSupplierExample();
            supplier.Telephone = new List<Telephones>((3375886));
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }
    }
}