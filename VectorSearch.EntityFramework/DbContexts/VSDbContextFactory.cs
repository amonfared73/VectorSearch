using Microsoft.EntityFrameworkCore;

namespace VectorSearch.EntityFramework.DbContexts
{
    public class VSDbContextFactory : IVSDbContextFactory
    {
        private readonly DbContextOptions _options;
        public VSDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }
        public VSDbContext CreateDbContext()
        {
            return new VSDbContext(_options);
        }
    }
}
