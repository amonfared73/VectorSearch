using Microsoft.EntityFrameworkCore;

namespace VectorSearch.EF.Contexts
{
    public class VectorSearchDbContextFactory
    {
        private readonly DbContextOptions _options;

        public VectorSearchDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public VectorSearchDbContext Create() => new VectorSearchDbContext(_options);
    }
}
