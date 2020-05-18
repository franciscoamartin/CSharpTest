using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BludataTest.Repositories
{
    public class SupplierMapping 
        : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId).IsRequired();
            builder.Property(x => x.Document).HasConversion(y => y.ToString(), v => new Document(v, GetDocumentType(v)));
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.RegisterTime).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Telephones).HasConversion(t => GetTelephonesString(t), telephoneString => GetTelephoneArray(telephoneString));
        }

        private string GetTelephonesString(string[] telephones)
        {
            var resultString = "";
            foreach (var telephone in telephones)
            {
                resultString += telephone + ",";                                
            }
            resultString = resultString.Remove(resultString.Length -1);
            return resultString;
        }

        private string[] GetTelephoneArray(string telephones)
        {
            var resultString = telephones.Split(',');
            return resultString;
        }

        private EDocumentType GetDocumentType(string v)
        {
            if (v.Length == 11)
                return EDocumentType.CPF;
            return EDocumentType.CNPJ;
        }
    }
    
}