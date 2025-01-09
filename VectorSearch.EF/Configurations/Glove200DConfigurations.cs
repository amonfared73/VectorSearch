using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class Glove200DConfigurations : IEntityTypeConfiguration<Glove200D>
    {
        public void Configure(EntityTypeBuilder<Glove200D> builder)
        {
            builder.ToTable("GloveThree", "dbo");
        }
    }
}
