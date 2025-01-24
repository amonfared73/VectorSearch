using Microsoft.EntityFrameworkCore;

namespace VectorSearch.EF.Contexts
{
    public class VectorSearchDbContextFactory : IDbContextFactory<VectorSearchDbContext>
    {
        private readonly DbContextOptions<VectorSearchDbContext> _options;

        public VectorSearchDbContextFactory(DbContextOptions<VectorSearchDbContext> options)
        {
            _options = options;
        }

        public VectorSearchDbContext CreateDbContext() => new VectorSearchDbContext(_options);

    }
}
