using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class Glove100DConfigurations : IEntityTypeConfiguration<Glove100D>
    {
        public void Configure(EntityTypeBuilder<Glove100D> builder)
        {
            builder.ToTable("GloveTwo", "dbo");
        }
    }
}
