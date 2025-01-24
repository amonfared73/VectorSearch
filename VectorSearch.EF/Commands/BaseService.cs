using Microsoft.EntityFrameworkCore;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Models;
using VectorSearch.EF.Contexts;

namespace VectorSearch.EF.Commands
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : DomainObject
    {
        private readonly IDbContextFactory<VectorSearchDbContext> _contextFactory;
        public BaseService(IDbContextFactory<VectorSearchDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public virtual async Task<IEnumerable<TEntity>> Execute()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }
    }
}
