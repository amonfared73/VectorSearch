using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using VectorSearch.EF.Contexts;

namespace VectorSearch.EF.Tools
{
    public interface IDbSetService
    {
        IQueryable<IWord> GetProperDbSet(SearchOptions searchOptions, VectorSearchDbContext context);
    }
}
