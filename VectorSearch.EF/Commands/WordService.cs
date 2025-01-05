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

        public async Task<IEnumerable<WordDto>> GetAllAsync(string searchText)
        {
            using (var context = _contextFactory.Create())
            {
                return await
                    context
                    .Words
                    .Where(x => string.IsNullOrEmpty(searchText) || x.Text.Contains(searchText))
                    .Select(w => new WordDto() { Id = w.Id, Text = w.Text, Vector = w.Vector })
                    .Take(15)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<WordDto>> GetAllSimilarWords(string searchText)
        {
            using (var context = _contextFactory.Create())
            {
                var searchWord = await context.Words.FirstOrDefaultAsync(w => w.Text == searchText);

                if (searchText == null || string.IsNullOrEmpty(searchWord.Vector))
                    return Enumerable.Empty<WordDto>();

                var targetVector = searchWord.Vector.ParseVector();

                var words = await context.Words.Select(w => new WordDto { Id = w.Id, Text = w.Text, Vector = w.Vector }).ToListAsync();

                return words
                    .Select(word => new
                    {
                        Word = word,
                        Similarity = VectorMath.ComputeCosineSimilarity(targetVector, word.Vector.ParseVector())
                    })
                    .OrderByDescending(x => x.Similarity)
                    .Take(15)
                    .Select(x => x.Word)
                    .ToList();
            }
        }
    }
}
