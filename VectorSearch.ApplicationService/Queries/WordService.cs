using VectorSearch.ApplicationService.Services;
using VectorSearch.Core.Models;
using VectorSearch.EntityFramework.DbContexts;

namespace VectorSearch.ApplicationService.Queries
{
    public class WordService : IWordService
    {
        private readonly IVSDbContextFactory _dbContextFactory;
        public WordService(IVSDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<IQueryable<Word>> GetAll(string? word)
        {
            throw new NotImplementedException();
        }

        public Task<Word> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(string word)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Word>> VectorSearch(string word)
        {
            throw new NotImplementedException();
        }
        public Task Delete(string word)
        {
            throw new NotImplementedException();
        }
    }
}
