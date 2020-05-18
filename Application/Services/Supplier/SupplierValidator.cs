using System;
using System.Text.RegularExpressions;
using BludataTest.Models;

namespace BludataTest.Services
{
    public class SupplierValidator
    {
        public bool isValid(Supplier supplier)
        {
            if(supplier.Company == null)
              return false;
            if(supplier.Company.UF == "PR" 
                       && supplier.Company.Document.ToString().Length == 11 
                       && !IsLegalAGe(supplier.BirthDate))
              return false;
            if(string.IsNullOrWhiteSpace(supplier.Name) || supplier.Name.Length < 3)
              return false; 
            if(supplier.BirthDate == null)
              return false;
            if(supplier.BirthDate > DateTime.Now || supplier.BirthDate < new DateTime(1910, 1, 1))
              return false;
            if(supplier.RegisterTime > DateTime.Now || supplier.RegisterTime < new DateTime(1910, 1, 1))
              return false;
            if(supplier.Telephones.Length <= 0 || !isValidTelephones(supplier.Telephones))
              return false;
            if(!isValidCPF(supplier.Document.ToString()))
              return false;
            return true;
        }

        private bool isValidTelephones(string[] telephones)
        {
          foreach (var telephone in telephones)
          {
            if(!Regex.IsMatch(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})", telephone))
              return false;
          }
          return true;
        }

        private bool IsLegalAGe(DateTime birthDate)
        {
          var days = 365;
          var leapYear = 4;
          var legalAge = 18;

          TimeSpan age = DateTime.Now - birthDate;       
          return age.Days / days + leapYear >= legalAge;    
        }

        private bool isValidCPF(string cpf)
        {
            //ValidadorCPF - Macoratti
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		        string tempCpf;
		        string digito;
		        int soma;
		        int resto;
		        cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for(int i=0; i<9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
            if ( resto < 2 )
                resto = 0;
            else
                resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
            for(int i=0; i<10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
                digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}