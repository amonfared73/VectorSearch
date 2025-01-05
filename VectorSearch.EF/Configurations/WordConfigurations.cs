using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class WordConfigurations : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder
                .ToTable("Words", "dbo");
        }
    }
}
