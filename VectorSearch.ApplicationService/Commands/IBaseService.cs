using VectorSearch.Domain.Models;

namespace VectorSearch.ApplicationService.Commands
{
    public interface IBaseService<TEntity> where TEntity : DomainObject
    {
        Task<IEnumerable<TEntity>> Execute();
    }
}
