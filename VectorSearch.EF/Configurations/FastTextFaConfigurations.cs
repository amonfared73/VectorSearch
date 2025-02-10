using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class FastTextFaConfigurations : IEntityTypeConfiguration<FastTextFa>
    {
        public void Configure(EntityTypeBuilder<FastTextFa> builder)
        {
            builder.ToTable("FastTextFa", "dbo");
        }
    }
}
