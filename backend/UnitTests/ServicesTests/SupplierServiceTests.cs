using System;
using System.Collections.Generic;
using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.Repositories;
using BludataTest.ResponseModels;
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
        private ICompanyService _companyService;
        private Supplier _supplierExample;
        private List<Supplier> _suppliersExample;
        public SupplierServicesTests()
        {
            _supplierRepository = Substitute.For<ISupplierRepository>();
            _companyService = Substitute.For<ICompanyService>();
            _supplierService = new SupplierService(_supplierRepository, _companyService);
            _supplierExample = GetSupplierExample();
            _suppliersExample = GetSuppliersExample();
        }

        private Company GetCompanyExample()
        {
            var company = new Company(uF: "SC", tradingName: "Mercado Chicão", cNPJ: "68.356.468/0001-57");
            company.Id = Guid.NewGuid();
            return company;
        }

        private List<Telephone> GetTelephonesExample()
        {
            var telephones = new List<Telephone>();
            telephones.Add(new Telephone("+554733375582"));
            telephones.Add(new Telephone("+554733784158"));
            telephones.Add(new Telephone("+5547992186559"));
            return telephones;
        }

        private Supplier GetSupplierExample()
        {
            var company = GetCompanyExample();
            var telephones = GetTelephonesExample();
            var supplier = new Supplier(name: "Ronaldo", company: company, companyId: company.Id, document: new Document("086.263.709-03", EDocumentType.CPF), rg: "623267", registerTime: DateTime.Now, birthDate: new DateTime(2001, 12, 7), telephone: GetTelephonesExample());
            return supplier;
        }

        private List<Supplier> GetSuppliersExample()
        {
            var suppliers = new List<Supplier>();
            suppliers.Add(GetSupplierExample());
            return suppliers;
        }

        [Fact]
        public void Should_create_supplier()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            _supplierService.Create(supplier);
            _supplierRepository.Received(1).Create(Arg.Is<Supplier>(s =>
             s.Name == "Ronaldo" &&
             s.Company.TradingName == "Mercado Chicão" &&
             s.Document.ToString() == "086.263.709-03" &&
             s.RG == "623267" &&
             s.RegisterTime.Date == DateTime.Now.Date &&
             s.BirthDate == new DateTime(2001, 12, 7) &&
             s.Telephones[0] == supplier.Telephones[0]));
        }

        [Fact]
        public void Should_not_create_supplier_when_name_is_wrong()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.Name = "L";

            var ex = Assert.Throws<Exception>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe o nome corretamente.");
        }

        [Fact]
        public void Should_not_create_supplier_when_birthDate_is_wrong()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.BirthDate = DateTime.Now.AddDays(1);

            var ex = Assert.Throws<Exception>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe uma data de nascimento válida.");
        }

        [Fact]
        public void Should_not_create_supplier_when_birthDate_is_before_1900()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.BirthDate = new DateTime(1899, 12, 31);

            var ex = Assert.Throws<Exception>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe uma data de nascimento válida.");
        }

        [Fact]
        public void Should_not_create_supplier_when_CPF_is_wrong()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.Document = new Document("083.27.709-03", EDocumentType.CPF);

            var ex = Assert.Throws<Exception>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe o documento corretamente.");
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
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            var telephoneToAdd = new Telephone("3375886");
            var telephoneList = new List<Telephone>();
            telephoneList.Add(telephoneToAdd);
            supplier.Telephones = telephoneList;

            var ex = Assert.Throws<Exception>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe um número de telefone válido");
        }

        [Fact]
        public void Should_get_supplier_response_models_list()
        {
            var supplierExample = GetSupplierExample();

            List<Supplier> suppliersToDb = new List<Supplier>();
            suppliersToDb.Add(supplierExample);
            _supplierRepository.GetByName(supplierExample.Name).Returns(suppliersToDb);

            var suppliersFound = _supplierService.GetByName(supplierExample.Name);
            var supResponseModel = suppliersFound[0];

            var formattedTelephones = supplierExample.Telephones.ConvertAll(tel => tel.Number);

            Assert.True(supResponseModel.CpfCnpj == supplierExample.Document.ToString()
                && supResponseModel.Name == supplierExample.Name
                && supResponseModel.BirthDate == supplierExample.BirthDate?.ToString("dd/MM/yyyy")
                && supResponseModel.Telephones[2] == formattedTelephones[2]
                && supResponseModel.RG == supplierExample.RG
                && supResponseModel.CompanyTradingName == supplierExample.Company.TradingName);
        }

        [Fact]
        public void Should_get_supplier_by_cpf()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByDocument(supplierExample.Document.ToString()).Returns(_suppliersExample);

            var suppliersFound = _supplierService.GetByDocument(supplierExample.Document.ToString());
            var supResponseModel = suppliersFound[0];

            Assert.True(supResponseModel.CpfCnpj == supplierExample.Document.ToString()
            && supResponseModel.Name == supplierExample.Name);
        }

        [Fact]
        public void Should_not_get_supplier_by_cpf()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByDocument(supplierExample.Document.ToString()).Returns(_suppliersExample);

            var ex = Assert.Throws<Exception>(() => _supplierService.GetByDocument("052.36.180-360"));

            Assert.Equal("O documento informado não é válido", ex.Message);
        }

        [Fact]
        public void Should_get_supplier_by_registerTime()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByRegisterTime(supplierExample.RegisterTime.Date).Returns(_suppliersExample);

            var suppliersFound = _supplierService.GetByRegisterTime(supplierExample.RegisterTime.Date.ToString("yyyy-MM-dd"));
            var supResponseModel = suppliersFound[0];

            Assert.True(supResponseModel.GetType() == typeof(SupplierResponseModel) && supResponseModel != null);
        }

        [Fact]
        public void Should_not_get_supplier_by_registerTime()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByRegisterTime(supplierExample.RegisterTime).Returns(_suppliersExample);

            var ex = Assert.Throws<Exception>(() => _supplierService.GetByRegisterTime(supplierExample.RegisterTime.Date.ToString()));

            Assert.Equal("Data de cadastro inválida", ex.Message);
        }
    }
}