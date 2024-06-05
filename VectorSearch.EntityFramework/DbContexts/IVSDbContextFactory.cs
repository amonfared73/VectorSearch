namespace VectorSearch.EntityFramework.DbContexts
{
    public interface IVSDbContextFactory
    {
        VSDbContext CreateDbContext();
    }
}
