using VectorSearch.Core.Models;
using VectorSearch.Core.ViewModels;

namespace VectorSearch.ApplicationService.Services
{
    public interface IWordService
    {
        Task<IQueryable<Word>> GetAll(string? word);
        Task<Word> GetById(int id);
        Task<IQueryable<Word>> VectorSearch(string word);
        Task Insert(string word);
        Task Delete(string word);
        Task ClearAllWords();
        Task BulkInsert(IEnumerable<WordViewModel> words);
    }
}
