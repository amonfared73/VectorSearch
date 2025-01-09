using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class DictionaryTypeConfigurations : IEntityTypeConfiguration<DictionaryType>
    {
        public void Configure(EntityTypeBuilder<DictionaryType> builder)
        {
            builder.ToTable("DictionaryTypes", "dbo");
        }
    }
}
