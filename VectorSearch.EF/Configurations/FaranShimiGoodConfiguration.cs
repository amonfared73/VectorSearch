using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class FaranShimiGoodConfiguration : IEntityTypeConfiguration<FaranShimiGood>
    {
        public void Configure(EntityTypeBuilder<FaranShimiGood> builder)
        {
            builder.ToTable("FaranShimiGoods", "dbo");
        }
    }
}
