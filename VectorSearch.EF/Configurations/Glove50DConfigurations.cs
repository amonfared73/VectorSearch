using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class Glove50DConfigurations : IEntityTypeConfiguration<Glove50D>
    {
        public void Configure(EntityTypeBuilder<Glove50D> builder)
        {
            builder.ToTable("GloveOne", "dbo");
        }
    }
}
