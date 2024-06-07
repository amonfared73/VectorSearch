using Microsoft.EntityFrameworkCore;
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
        public async Task<IQueryable<Word>> GetAll(string? word)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var words = await context
                    .Words
                    .Where(w => string.IsNullOrEmpty(word) || w.Text.Contains(word))
                    .ToListAsync();

                return words.AsQueryable();
            }
        }

        public async Task<Word> GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var word = await context
                    .Words
                    .FirstOrDefaultAsync(w => w.Id == id);

                return word;
            }
        }

        public async Task Insert(string word)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var words = context.Set<Word>();
                await words.AddAsync(new Word()
                {
                    Text = word,
                });
                await context.SaveChangesAsync();
            }
        }

        public Task<IQueryable<Word>> VectorSearch(string word)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string word)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var selectedWord = await context.Words.FirstOrDefaultAsync(w => w.Text.Contains(word));

                if (selectedWord != null)
                    throw new Exception($"Word: {word} not found!");

                context.Words.Remove(selectedWord);
                await context.SaveChangesAsync();
            }
        }
    }
}
