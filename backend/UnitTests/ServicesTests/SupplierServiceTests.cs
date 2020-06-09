using System;
using System.Collections.Generic;
using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.Repositories;
using BludataTest.ResponseModels;
using BludataTest.Services;
using BludataTest.ValueObject;
using BludataTest.CustomExceptions;
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
            var supplier = new Supplier(name: "Ronaldo", company: company, companyId: company.Id, document: new Document("086.263.709-03", EDocumentType.CPF), rg: "623267", registerTime: DateTime.Now, birthDate: new DateTime(2001, 12, 7), telephone: telephones);
            supplier.Id = Guid.NewGuid();
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

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe o nome corretamente.");
        }

        [Fact]
        public void Should_not_create_supplier_when_birthDate_is_wrong()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.BirthDate = DateTime.Now.AddDays(1);

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe uma data de nascimento válida.");
        }

        [Fact]
        public void Should_not_create_supplier_when_birthDate_is_before_1900()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.BirthDate = new DateTime(1899, 12, 31);

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe uma data de nascimento válida.");
        }

        [Fact]
        public void Should_not_create_supplier_when_rg_is_wrong()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.RG = "623";

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe um RG válido.");
        }

        [Fact]
        public void Should_not_create_supplier_when_CPF_is_wrong()
        {
            var supplier = GetSupplierExample();
            _companyService.Read(supplier.CompanyId).Returns(GetCompanyExample());
            supplier.Document = new Document("083.27.709-03", EDocumentType.CPF);

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe o documento corretamente.");
        }

        [Fact]
        public void Should_not_create_supplier_when_CNPJ_is_wrong()
        {
            var supplier = GetSupplierExample();
            supplier.Document = new Document("18.321.410/001-42", EDocumentType.CNPJ);
            Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));
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

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Create(supplier));

            Assert.True(ex.Message == "Informe um número de telefone válido");
        }

        [Fact]
        public void Should_get_supplier_response_models_list()
        {
            var supplierExample = GetSupplierExample();

            List<Supplier> suppliersToDb = new List<Supplier>();
            suppliersToDb.Add(supplierExample);
            _supplierRepository.GetByName(supplierExample.Name).Returns(suppliersToDb);

            var foundSuppliers = _supplierService.GetByName(supplierExample.Name);
            var supResponseModel = foundSuppliers[0];

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

            var foundSuppliers = _supplierService.GetByDocument(supplierExample.Document.ToString());
            var supResponseModel = foundSuppliers[0];

            Assert.True(supResponseModel.CpfCnpj == supplierExample.Document.ToString()
            && supResponseModel.Name == supplierExample.Name);
        }

        [Fact]
        public void Should_not_get_supplier_by_cpf()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByDocument(supplierExample.Document.ToString()).Returns(_suppliersExample);

            var ex = Assert.Throws<ValidationException>(() => _supplierService.GetByDocument("052.36.180-360"));

            Assert.Equal("O documento informado não é válido", ex.Message);
        }

        [Fact]
        public void Should_get_supplier_by_registerTime()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByRegisterTime(supplierExample.RegisterTime.Date).Returns(_suppliersExample);

            var foundSuppliers = _supplierService.GetByRegisterTime(supplierExample.RegisterTime.Date.ToString("yyyy-MM-dd"));
            var supResponseModel = foundSuppliers[0];

            Assert.True(supResponseModel.GetType() == typeof(SupplierResponseModel) && supResponseModel != null);
        }

        [Fact]
        public void Should_not_get_supplier_by_registerTime()
        {
            var supplierExample = GetSupplierExample();
            _supplierRepository.GetByRegisterTime(supplierExample.RegisterTime).Returns(_suppliersExample);

            var ex = Assert.Throws<ValidationException>(() => _supplierService.GetByRegisterTime(supplierExample.RegisterTime.Date.ToString()));

            Assert.Equal("Data de cadastro inválida", ex.Message);
        }

        [Fact]
        public void Should_update_supplier()
        {
            var supplierDb = GetSupplierExample();
            _supplierRepository.GetById(supplierDb.Id).Returns(supplierDb);
            var supplierToUpdate = GetSupplierExample();
            supplierToUpdate.Name = "João da Silva";
            supplierToUpdate.Telephones[0] = new Telephone("+554733784158");

            _supplierService.Update(supplierDb.Id, supplierToUpdate);

            _supplierRepository.Received(1).Update(Arg.Is<Supplier>(s =>
             s.Name == "João da Silva" &&
             s.Company.TradingName == "Mercado Chicão" &&
             s.Document.ToString() == "086.263.709-03" &&
             s.RG == "623267" &&
             s.RegisterTime.Date == DateTime.Now.Date &&
             s.BirthDate == new DateTime(2001, 12, 7) &&
             s.Telephones[0].Number == supplierToUpdate.Telephones[0].Number));
        }

        [Fact]
        public void Should_not_update_supplier_when_telephone_is_wrong()
        {
            var supplierDb = GetSupplierExample();
            _supplierRepository.GetById(supplierDb.Id).Returns(supplierDb);
            var supplierToUpdate = GetSupplierExample();
            supplierToUpdate.Telephones[0] = new Telephone("4733784158");

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Update(supplierDb.Id, supplierToUpdate));
            Assert.Equal("Informe um número de telefone válido", ex.Message);
        }

        [Fact]
        public void Should_not_update_supplier_when_name_is_wrong()
        {
            var supplierDb = GetSupplierExample();
            _supplierRepository.GetById(supplierDb.Id).Returns(supplierDb);
            var supplierToUpdate = GetSupplierExample();
            supplierToUpdate.Name = "Jô";

            var ex = Assert.Throws<ValidationException>(() => _supplierService.Update(supplierDb.Id, supplierToUpdate));
            Assert.Equal("Informe o nome corretamente.", ex.Message);
        }

        [Fact]
        public void Should_delete_supplier()
        {
            var supplierDb = GetSupplierExample();
            _supplierRepository.GetById(supplierDb.Id).Returns(supplierDb);

            _supplierService.Delete(supplierDb.Id);

            _supplierRepository.Received(1).Delete(Arg.Is<Supplier>(s =>
             s.Name == "Ronaldo" &&
             s.Company.TradingName == "Mercado Chicão" &&
             s.Document.ToString() == "086.263.709-03" &&
             s.RG == "623267" &&
             s.RegisterTime.Date == DateTime.Now.Date &&
             s.BirthDate == new DateTime(2001, 12, 7) &&
             s.Telephones[0] == supplierDb.Telephones[0]));
        }

        [Fact]
        public void Should_not_delete_supplier_when_id_is_empty()
        {
            var ex = Assert.Throws<ValidationException>(() => _supplierService.Delete(new Guid()));
            Assert.Equal("O fornecedor precisa ser informado.", ex.Message);
        }
    }
}