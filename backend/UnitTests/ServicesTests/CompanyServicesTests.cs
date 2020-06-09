using System;
using Xunit;
using NSubstitute;
using BludataTest.Services;
using BludataTest.Repositories;
using BludataTest.Models;
using BludataTest.ValueObject;
using BludataTest.CustomExceptions;
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

        private Company GetCompanyExample()
        {
            var company = new Company(uF: "SC", tradingName: "Mercado Chicão", cNPJ: "68.356.468/0001-57");
            company.Id = Guid.NewGuid();
            return company;
        }

        [Fact]
        public void Should_create_company()
        {
            var company = GetCompanyExample();
            _companyService.Create(company);
            _companyRepository.Received(1).Create(Arg.Is<Company>(c => c.UF == "SC"
                                                                  && c.TradingName == "Mercado Chicão"
                                                                  && c.CNPJ == "68.356.468/0001-57"));
        }

        [Fact]
        public void Should_not_create_company_when_uf_is_wrong()
        {
            var company = GetCompanyExample();
            company.UF = "L";
            Assert.Throws<ValidationException>(() => _companyService.Create(company));
        }

        [Fact]
        public void Should_not_create_company_when_tradingName_is_wrong()
        {
            var company = GetCompanyExample();
            company.TradingName = "BL";
            Assert.Throws<ValidationException>(() => _companyService.Create(company));
        }

        [Fact]
        public void Should_not_create_company_when_CNPJ_is_wrong()
        {
            var company = GetCompanyExample();
            company.CNPJ = "086.263.710-03";
            Assert.Throws<ValidationException>(() => _companyService.Create(company));
        }

        [Fact]
        public void Should_get_company_by_id()
        {
            var company = GetCompanyExample();
            var companyExampleId = Guid.NewGuid();
            company.Id = companyExampleId;
            _companyRepository.GetById(companyExampleId).Returns(company);
            var companyReceived = _companyService.Read(companyExampleId);
            Assert.Equal(company.TradingName, companyReceived.TradingName);
        }

        [Fact]
        public void Should_not_get_company_by_id()
        {
            var company = GetCompanyExample();
            Assert.Throws<ValidationException>(() => _companyService.Read(Guid.Empty));
        }

        [Fact]
        public void Should_update_company()
        {
            var companyDb = GetCompanyExample();
            _companyRepository.GetById(companyDb.Id).Returns(companyDb);
            var companyToUpdate = GetCompanyExample();
            companyToUpdate.TradingName = "Mercado Chiquinho";
            companyToUpdate.UF= "RS";

            _companyService.Update(companyDb.Id, companyToUpdate);

            _companyRepository.Received(1).Update(Arg.Is<Company>(c => c.UF == "RS"
                                                                  && c.TradingName == "Mercado Chiquinho"
                                                                  && c.CNPJ == "68.356.468/0001-57"));
        }

        [Fact]
        public void Should_not_update_company_when_uf_is_wrong()
        {
            var companyDb = GetCompanyExample();
            _companyRepository.GetById(companyDb.Id).Returns(companyDb);
            var companyToUpdate = GetCompanyExample();
            companyToUpdate.UF = "";

            var ex = Assert.Throws<ValidationException>(() => _companyService.Update(companyDb.Id, companyToUpdate));
            Assert.Equal("O estado não é válido.", ex.Message);
        }

        [Fact]
        public void Should_not_update_company_when_trading_name_is_wrong()
        {
            var companyDb = GetCompanyExample();
            _companyRepository.GetById(companyDb.Id).Returns(companyDb);
            var companyToUpdate = GetCompanyExample();
            companyToUpdate.TradingName = "ab";

            var ex = Assert.Throws<ValidationException>(() => _companyService.Update(companyDb.Id, companyToUpdate));
            Assert.Equal("Informe o nome fantasia corretamente", ex.Message);
        }

        [Fact]
        public void Should_delete_company()
        {
            var companyDb = GetCompanyExample();
            _companyRepository.GetById(companyDb.Id).Returns(companyDb);

            _companyService.Delete(companyDb.Id);

            _companyRepository.Received(1).Delete(Arg.Is<Company>(c => c.UF == "SC"
                                                                  && c.TradingName == "Mercado Chicão"
                                                                  && c.CNPJ == "68.356.468/0001-57"));
        }

        [Fact]
        public void Should_not_delete_company_when_id_is_empty()
        {
            var ex = Assert.Throws<ValidationException>(() => _companyService.Delete(new Guid()));
            Assert.Equal("Informe a empresa", ex.Message);
        }
    }
}