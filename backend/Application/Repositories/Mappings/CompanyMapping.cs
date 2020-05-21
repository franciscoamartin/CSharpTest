using BludataTest.Enums;
using BludataTest.Models;
using BludataTest.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BludataTest.Repositories
{
    public class CompanyMapping 
        : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.Document).HasConversion(y => y.ToString(), v => new Document(v, EDocumentType.CNPJ));
            builder.Property(x => x.UF).IsRequired().HasMaxLength(2);
        }
    }
}