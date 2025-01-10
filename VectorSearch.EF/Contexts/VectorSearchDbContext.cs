using Microsoft.EntityFrameworkCore;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Contexts
{
    public class VectorSearchDbContext : DbContext
    {
        public VectorSearchDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VectorSearchDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DictionaryType> DictionaryTypes { get; set; }
        public DbSet<Glove50D> Glove50Ds { get; set; }
        public DbSet<Glove100D> Glove100Ds { get; set; }
        public DbSet<Glove200D> Glove200Ds { get; set; }
        public DbSet<Glove300D> Glove300Ds { get; set; }
    }
}
