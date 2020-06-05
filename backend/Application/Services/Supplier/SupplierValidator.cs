using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.ValueObject;

namespace BludataTest.Services
{
    public class SupplierValidator
    {
        private readonly DocumentValidator _documentValidator;

        public SupplierValidator()
        {
            _documentValidator = new DocumentValidator();
        }
        public void Validate(Supplier supplier)
        {
            if (supplier == null)
                throw new Exception("Fornecedor não informado!");
            if (supplier.CompanyId == Guid.Empty)
                throw new Exception("Empresa não informada!");
            if (supplier.Document.Type == EDocumentType.CNPJ && !string.IsNullOrWhiteSpace(supplier.RG))
                throw new Exception("Pessoa jurídica não deve possuir RG");

            ValidateSupplierWithCPF(supplier);
            ValidateSupplierFromPR(supplier);
            ValidateName(supplier.Name);
            ValidateBirthDate(supplier.BirthDate);
            ValidateRegisterTime(supplier.RegisterTime);
            ValidateTelephones(supplier.Telephones);
            ValidateDocument(supplier.Document);
        }

        private void ValidateSupplierWithCPF(Supplier supplier)
        {
            if (supplier.Document.Type == EDocumentType.CPF && (supplier.BirthDate == null || supplier.RG == null))
                throw new Exception("Data de nascimento e RG precisam ser informados.");
        }

        private void ValidateSupplierFromPR(Supplier supplier)
        {
            if (supplier.Company.UF == "PR"
                       && supplier.Document.Type == EDocumentType.CPF
                       && !isLegalAGe(supplier.BirthDate))
                throw new Exception("O fornecedor, sendo do Paraná, precisa ser maior de idade.");
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new Exception("Informe o nome corretamente.");
        }

        private void ValidateBirthDate(DateTime? birthDate)
        {
            if (birthDate > DateTime.Now || birthDate < new DateTime(1910, 1, 1))
                throw new Exception("Informe uma data de nascimento válida.");
        }

        private void ValidateRegisterTime(DateTime registerTime)
        {
            if (registerTime > DateTime.Now || registerTime < new DateTime(1910, 1, 1))
                throw new Exception("Data de cadastro inválida.");
        }

        public void ValidateTelephones(List<Telephone> telephones)
        {
            string pattern = @"(\+\d{2})(\s)?(\()?\d{2}(\)?)(\s)?\d{4,5}(\-)?\d{4}";
            var regex = new Regex(pattern);
            foreach (var telephone in telephones)
            {
                if (!regex.IsMatch(telephone.Number))
                    throw new Exception("Informe um número de telefone válido");
            }
        }

        private void ValidateDocument(Document document)
        {
            if (!_documentValidator.isValid(document))
                throw new Exception("Informe o documento corretamente.");
        }

        private bool isLegalAGe(DateTime? birthDate)
        {
            var fullYearDays = 365;
            var leapYear = 4;
            var legalAge = 18;

            TimeSpan? age = DateTime.Now - birthDate;
            return age?.Days / (fullYearDays + leapYear) >= legalAge;
        }
    }
}