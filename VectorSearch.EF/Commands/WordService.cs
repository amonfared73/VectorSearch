using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using VectorSearch.EF.Contexts;
using VectorSearch.EF.Tools;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly VectorSearchDbContextFactory _contextFactory;
        public WordService(VectorSearchDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<WordDto>> GetAllAsync(SearchOptions searchOptions)
        {
            using (var context = _contextFactory.Create())
            {
                return await
                    context
                    .Words
                    .Where(x => string.IsNullOrEmpty(searchOptions.Text) || x.Text.Contains(searchOptions.Text))
                    .Select(w => new WordDto() { Id = w.Id, Text = w.Text, Vector = w.Vector })
                    .Skip((searchOptions.PageNumber - 1) * searchOptions.PageSize)
                    .Take(searchOptions.PageSize)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<WordDto>> GetAllSimilarWords(SearchOptions searchOptions)
        {
            using (var context = _contextFactory.Create())
            {
                var searchWord = await context.Words.FirstOrDefaultAsync(w => w.Text == searchOptions.Text);

                if (searchOptions.Text == null || string.IsNullOrEmpty(searchWord.Vector))
                    return Enumerable.Empty<WordDto>();

                var targetVector = searchWord.Vector.ParseVector();

                var words = await context.Words.Select(w => new WordDto { Id = w.Id, Text = w.Text, Vector = w.Vector }).ToListAsync();

                return words
                    .Select(word => 
                    {
                        word.Similarity = VectorMath.ComputeCosineSimilarity(targetVector, word.Vector.ParseVector());
                        return word;
                    })
                    .OrderByDescending(x => x.Similarity)
                    .Skip((searchOptions.PageNumber - 1) * searchOptions.PageSize)
                    .Take(searchOptions.PageSize)
                    .ToList();
            }
        }
    }
}
