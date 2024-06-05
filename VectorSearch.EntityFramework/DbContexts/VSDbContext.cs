using Microsoft.EntityFrameworkCore;
using VectorSearch.Core.Models;

namespace VectorSearch.EntityFramework.DbContexts
{
    public class VSDbContext : DbContext
    {
        public VSDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Word> Words { get; set; }
    }
}
