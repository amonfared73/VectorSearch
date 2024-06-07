using VectorSearch.Core.Models;

namespace VectorSearch.ApplicationService.Services
{
    public interface IWordService
    {
        Task<IQueryable<Word>> GetAll();
        Task<Word> GetById(int id);
        Task<IQueryable<Word>> VectorSearch(string word);
        Task Insert(string word);
        Task Delete(string word);
    }
}
