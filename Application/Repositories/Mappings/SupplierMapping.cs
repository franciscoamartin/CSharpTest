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
        }

        private EDocumentType GetDocumentType(string v)
        {
            if (v.Length == 11)
                return EDocumentType.CPF;
            return EDocumentType.CNPJ;
        }
    }
    
}