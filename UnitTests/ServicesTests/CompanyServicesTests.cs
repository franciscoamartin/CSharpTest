using System;
using Xunit;
using NSubstitute;
using BludataTest.Services;
using BludataTest.Repositorio;
using BludataTest.Models;
using BludataTest.ValueObject;
using BludataTest.Enums;

namespace UnitTests.ServicesTests
{
    public class ServicesTests
    {
        private ICompanyService _companyService;
        private ICompanyRepository _companyRepository;        
        public ServicesTests()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();
            _companyService = new CompanyService(_companyRepository);
        }   

        [Fact]
        public void Should_create_company()
        {
            var company = new Company(uf: "SC", tradingName: "Mercado Chicão", document: new Document("68.356.468/0001-57", EDocumentType.CNPJ));
            _companyService.Create(company);
            _companyRepository.Received(1).Create(Arg.Is<Company>(c => c.UF=="SC" 
                                                                  && c.TradingName=="Mercado Chicão" 
                                                                  && c.Document.ToString()=="68.356.468/0001-57"));
        }

        [Fact]
        public void Should_not_create_company()
        {
            var company = new Company(uf: "SC", tradingName: "Mercado Kennedy", document: new Document("68.356.468/002-57", EDocumentType.CNPJ));
            Assert.Throws<Exception>(() => _companyService.Create(company));
        }        
    }
}