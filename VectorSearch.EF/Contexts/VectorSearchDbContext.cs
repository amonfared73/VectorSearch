using Microsoft.EntityFrameworkCore;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Contexts
{
    public class VectorSearchDbContext : DbContext
    {
        public VectorSearchDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Word> Words { get; set; }
    }
}
