using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VectorSearch.EF.Contexts
{
    public class VectorSearchDesignTimeDbContextFactory : IDesignTimeDbContextFactory<VectorSearchDbContext>
    {
        public VectorSearchDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Database=VectorSearch;Trusted_Connection=True;TrustServerCertificate=True;";
            var optionsBuilder = new DbContextOptionsBuilder<VectorSearchDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new VectorSearchDbContext(optionsBuilder.Options);
        }
    }
}
