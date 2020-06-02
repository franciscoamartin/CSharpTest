using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BludataTest.Enums;
using BludataTest.Models;

namespace BludataTest.Services
{
    public class SupplierValidator
    {
        private readonly DocumentValidator _documentValidator;

        public SupplierValidator()
        {
            _documentValidator = new DocumentValidator();
        }
        public bool Validate(Supplier supplier)
        {
            if (supplier == null)
                throw new Exception("Fornecedor não informado!");
            if (supplier.Company == null)
                throw new Exception("Empresa não informada!");
            if (supplier.Document.Type == EDocumentType.CNPJ && !string.IsNullOrWhiteSpace(supplier.RG))
                throw new Exception("");
            if (supplier.Company.UF == "PR"
                       && supplier.Document.Type == EDocumentType.CPF
                       && !isLegalAGe(supplier.BirthDate))
                throw new Exception("O fornecedor precisa ser maior de idade.");
            if (string.IsNullOrWhiteSpace(supplier.Name) || supplier.Name.Length < 3)
                throw new Exception("Informe o nome corretamente.");
            if (!(supplier.Document.Type == EDocumentType.CPF) && (supplier.BirthDate == null || supplier.RG == null))
                throw new Exception("Data de nascimento e RG precisam ser informados.");
            if (supplier.BirthDate > DateTime.Now || supplier.BirthDate < new DateTime(1910, 1, 1))
                throw new Exception("Informe uma data de nascimento válida.");
            if (supplier.RegisterTime > DateTime.Now || supplier.RegisterTime < new DateTime(1910, 1, 1))
                throw new Exception("");
            if (!isValidTelephones(supplier.Telephone))
                throw new Exception("Informe um número de telefone válido");
            if (_documentValidator.isValid(supplier.Document))
                throw new Exception("Informe o documento corretamente.");
            return true;
        }

        private bool isValidTelephones(List<Telephones> telephones)
        {
            foreach (var telephone in telephones)
            {
                if (!Regex.IsMatch(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})", telephone.Number))
                    return false;
            }
            return true;
        }

        private bool isLegalAGe(DateTime? birthDate)
        {
            var days = 365;
            var leapYear = 4;
            var legalAge = 18;

            TimeSpan? age = DateTime.Now - birthDate;
            return age?.Days / days + leapYear >= legalAge;
        }
    }
}