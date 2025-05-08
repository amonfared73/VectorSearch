using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class PadideShimiGharbGoodConfiguration : IEntityTypeConfiguration<PadideShimiGharbGood>
    {
        public void Configure(EntityTypeBuilder<PadideShimiGharbGood> builder)
        {
            builder.ToTable("PadidehShimiGharbGoods","dbo");
        }
    }
}
