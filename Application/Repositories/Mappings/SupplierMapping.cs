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
            builder.Property(x => x.Document).HasConversion(y => y.ToString(), v => new Document(v, GetDocumentType(v)));
        }

        private EDocumentType GetDocumentType(string v)
        {
            if (v.Length == 11)
            {
                return EDocumentType.CPF;
            }
            return EDocumentType.CNPJ;
        }
    }
    
}