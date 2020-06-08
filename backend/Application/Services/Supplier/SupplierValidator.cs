using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BludataTest.CustomExceptions;
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
        public void ValidateSupplier(Supplier supplier)
        {
            if (supplier.CompanyId == Guid.Empty)
                throw new ValidationException("Empresa não informada!");
            ValidateSupplierWithCNPJ(supplier);
            ValidateSupplierWithCPF(supplier);
            ValidateSupplierWithCompanyFromPR(supplier);
            ValidateName(supplier.Name);
            ValidateBirthDate(supplier.BirthDate);
            ValidateRegisterTime(supplier.RegisterTime);
            ValidateTelephones(supplier.Telephones);
            ValidateDocument(supplier.Document);
        }

        private void ValidateSupplierWithCNPJ(Supplier supplier)
        {
            if (supplier.Document.Type == EDocumentType.CNPJ && (!string.IsNullOrWhiteSpace(supplier.RG) || supplier.BirthDate != null))
                throw new ValidationException("Pessoa jurídica não deve possuir RG nem data de nascimento");
        }

        private void ValidateSupplierWithCPF(Supplier supplier)
        {
            if (supplier.Document.Type == EDocumentType.CPF && (supplier.BirthDate == null || supplier.RG == null))
                throw new ValidationException("Data de nascimento e RG precisam ser informados.");
        }

        private void ValidateSupplierWithCompanyFromPR(Supplier supplier)
        {
            if (supplier.Company.UF == "PR"
                       && supplier.Document.Type == EDocumentType.CPF
                       && !isLegalAGe(supplier.BirthDate))
                throw new ValidationException("O fornecedor, sendo do Paraná, precisa ser maior de idade.");
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                throw new ValidationException("Informe o nome corretamente.");
        }

        private void ValidateBirthDate(DateTime? birthDate)
        {
            if (birthDate == null || birthDate > DateTime.Now || birthDate < new DateTime(1910, 1, 1))
                throw new ValidationException("Informe uma data de nascimento válida.");
        }

        private void ValidateRegisterTime(DateTime registerTime)
        {
            if (registerTime > DateTime.Now || registerTime < new DateTime(1910, 1, 1))
                throw new ValidationException("Data de cadastro inválida.");
        }

        public void ValidateTelephones(List<Telephone> telephones)
        {
            string pattern = @"(\+\d{2})(\s)?(\()?\d{2}(\)?)(\s)?\d{4,5}(\-)?\d{4}";
            var regex = new Regex(pattern);
            foreach (var telephone in telephones)
            {
                if (!regex.IsMatch(telephone.Number))
                    throw new ValidationException("Informe um número de telefone válido");
            }
        }

        private void ValidateDocument(Document document)
        {
            if (!_documentValidator.isValid(document))
                throw new ValidationException("Informe o documento corretamente.");
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