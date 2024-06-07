using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VectorSearch.EntityFramework.DbContexts
{
    public class VSDbContextDesignTime : IDesignTimeDbContextFactory<VSDbContext>
    {
        public VSDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=VectorSearch.db";
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlite(connectionString);
            return new VSDbContext(optionsBuilder.Options);
        }
    }
}
