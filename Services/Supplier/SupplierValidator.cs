using System;
using BludataTest.Models;

namespace BludataTest.Services
{
    public class SupplierValidator
    {
        public bool isValid(Supplier supplier)
        {
            TimeSpan idade = DateTime.Now - supplier.BirthDate;               
            if(supplier.Company.UF.ToString() == "PR" && supplier.Company.Document.ToString().Length == 11 && (idade.Days / 365) + 4 >= 18 )
              return false;
            if(supplier.Company == null)
              return false;
            if(supplier.Name == null || supplier.Name.Length < 2)
              return false; 
            if(supplier.Telephone == null || supplier.Telephone.Length < 8)
              return false;
            if(supplier.BirthDate == null)
              return false;
            if(supplier.BirthDate > DateTime.Now || supplier.BirthDate < new DateTime(1910, 1, 1))
              return false;
            //if(supplier.RegisterTime > DateTime.Now || supplier.RegisterTime < new DateTime(1910, 1, 1))
              //return false;
            return true;
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

        private bool isValidCNPJ(string cnpj)
        {
            return false;
        }

        private bool isValidRG(string rg)
        {
            return false;
        }
    }
}