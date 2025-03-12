using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Configurations
{
    public class WikipediaFarsiConfigurations : IEntityTypeConfiguration<WikipediaFarsi>
    {
        public void Configure(EntityTypeBuilder<WikipediaFarsi> builder)
        {
            builder.ToTable("WikipediaFarsi", "dbo");
        }
    }
}
