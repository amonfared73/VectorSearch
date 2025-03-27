using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class DigikalaGoodConfigurations : IEntityTypeConfiguration<DigikalaGood>
    {
        public void Configure(EntityTypeBuilder<DigikalaGood> builder)
        {
            builder.ToTable("DigikalaGoods", "dbo");
        }
    }
}
