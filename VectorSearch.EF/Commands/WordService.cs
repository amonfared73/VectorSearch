using Microsoft.EntityFrameworkCore;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using VectorSearch.Domain.ViewModels;
using VectorSearch.EF.Contexts;
using VectorSearch.EF.Tools;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly IMathService _mathService;
        private readonly VectorSearchOptions _options;
        private readonly VectorSearchDbContextFactory _contextFactory;
        public WordService(VectorSearchDbContextFactory contextFactory, IMathService mathService, VectorSearchOptions options) : base(contextFactory)
        {
            _options = options;
            _mathService = mathService;
            _contextFactory = contextFactory;
        }

        public async Task<PagedResult<WordDto>> GetAllAsync(SearchOptions searchOptions)
        {
            using (var context = _contextFactory.Create())
            {
                var query = context
                    .Words
                    .Where(x => string.IsNullOrEmpty(searchOptions.Text) || x.Text.Contains(searchOptions.Text));

                var totalRecords = await query.CountAsync();

                var data = await query
                    .OrderBy(x => x.Id)
                    .Skip((searchOptions.PageNumber - 1) * searchOptions.PageSize)
                    .Take(searchOptions.PageSize)
                    .Select(w => new WordDto()
                    {
                        Id = w.Id,
                        Text = w.Text,
                        Vector = w.Vector,
                    })
                    .ToListAsync();

                var totalPages = (int)Math.Ceiling((double)totalRecords / searchOptions.PageSize);

                return new PagedResult<WordDto>()
                {
                    Data = data,
                    CurrentPage = searchOptions.PageNumber,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords
                };
            }
        }

        public async Task<PagedResult<WordDto>> GetAllSimilarWords(SearchOptions searchOptions)
        {
            if (string.IsNullOrEmpty(searchOptions.Text))
            {
                return new PagedResult<WordDto>
                {
                    Data = new List<WordDto>(),
                    CurrentPage = searchOptions.PageNumber,
                    TotalPages = 0,
                    TotalRecords = 0
                };
            }

            using (var context = _contextFactory.Create())
            {
                var searchWord = await context.Words
                    .Where(w => w.Text == searchOptions.Text)
                    .Select(w => new { w.Text, w.Vector })
                    .FirstOrDefaultAsync();

                if (searchWord == null || string.IsNullOrEmpty(searchWord.Vector))
                {
                    return new PagedResult<WordDto>
                    {
                        Data = new List<WordDto>(),
                        CurrentPage = searchOptions.PageNumber,
                        TotalPages = 0,
                        TotalRecords = 0
                    };
                }

                var targetVector = searchWord.Vector.ParseVector();

                var words = await context.Words
                    .Where(w => !string.IsNullOrEmpty(w.Vector)) // Ensuring the vector exists
                    .Select(w => new { w.Id, w.Text, w.Vector })
                    .ToListAsync();

                var similarWords = words
                    .Select(word =>
                    {
                        var wordVector = word.Vector.ParseVector();
                        var similarity = _mathService.ComputeCosineSimilarity(targetVector, wordVector);
                        return new WordDto
                        {
                            Id = word.Id,
                            Text = word.Text,
                            Vector = word.Vector,
                            Similarity = similarity
                        };
                    })
                    .OrderByDescending(word => word.Similarity);

                var filteredWords = similarWords.Where(word => word.Similarity > Convert.ToDouble(_options.SimilarityThreshold));

                var totalRecords = filteredWords.Count();

                var pagedData = filteredWords
                    .Skip((searchOptions.PageNumber - 1) * searchOptions.PageSize)
                    .Take(searchOptions.PageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling((double)totalRecords / searchOptions.PageSize);

                return new PagedResult<WordDto>
                {
                    Data = pagedData,
                    CurrentPage = searchOptions.PageNumber,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords
                };
            }
        }

    }
}
