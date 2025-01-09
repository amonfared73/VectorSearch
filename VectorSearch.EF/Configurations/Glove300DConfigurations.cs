using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class Glove300DConfigurations : IEntityTypeConfiguration<Glove300D>
    {
        public void Configure(EntityTypeBuilder<Glove300D> builder)
        {
            builder.ToTable("GloveFour", "dbo");
        }
    }
}
