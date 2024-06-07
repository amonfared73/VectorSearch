using VectorSearch.Core.Models;

namespace VectorSearch.ApplicationService.Services
{
    public interface IWordService
    {
        Task<IQueryable<Word>> GetAll(string? word);
        Task<Word> GetById(int id);
        Task<IQueryable<Word>> VectorSearch(string word);
        Task Insert(string word);
        Task Delete(string word);
    }
}
