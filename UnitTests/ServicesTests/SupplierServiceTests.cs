using System;
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
            var company = new Company(uF: "SC", tradingName: "Mercado Chicão", document: new Document("68.356.468/0001-57", EDocumentType.CNPJ));
            return company;       
        }

        private Supplier GetSupplierExample()
        {
            var company = GetCompanyExample();
            var supplier =  new Supplier(name: "Ronaldo", company: company, companyId: company.Id, document: new Document("086.263.709-03",  EDocumentType.CPF), registerTime: DateTime.Now, birthDate: new DateTime(2001,12,7), telephones: new string[] {"4733784158"});
            return supplier;       
        }

        [Fact]
        public void Should_create_supplier()
        {
            var supplier = GetSupplierExample();
            _supplierService.Create(supplier);
            _supplierRepository.Received(1).Create(Arg.Is<Supplier>(s => s.Name=="Ronaldo" 
                                                                  && s.Company.ToString()=="Mercado Chicão"
                                                                  && s.Document.ToString()=="086.263.709-03"
                                                                  && s.RegisterTime==DateTime.Now
                                                                  && s.BirthDate== new DateTime(2001,12,7)));
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
            supplier.BirthDate = new DateTime(1899,12,31);
            Assert.Throws<Exception>(() => _supplierService.Create(supplier));
        }
    }
}