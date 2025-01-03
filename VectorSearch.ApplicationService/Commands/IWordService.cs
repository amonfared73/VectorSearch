using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;

namespace VectorSearch.ApplicationService.Commands
{
    public interface IWordService : IBaseService<Word>
    {
        Task<IEnumerable<WordDto>> GetAllAsync();
    }
}
