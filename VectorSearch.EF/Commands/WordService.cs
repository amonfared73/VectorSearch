using Microsoft.EntityFrameworkCore;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using VectorSearch.EF.Contexts;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly VectorSearchDbContextFactory _contextFactory;
        public WordService(VectorSearchDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<WordDto>> GetAllAsync(string searchText)
        {
            using (var context = _contextFactory.Create())
            {
                var options = new SearchOptions();
                return await context.Words.Where(x => string.IsNullOrEmpty(searchText) || x.Text.Contains(searchText)).Select(w => new WordDto() { Id = w.Id, Text = w.Text, Vector = string.Join(", ", w.Vector)}).Take(15).ToListAsync();
            }
        }
    }
}
