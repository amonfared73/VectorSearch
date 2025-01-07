using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using VectorSearch.Domain.ViewModels;
using VectorSearch.EF.Contexts;
using VectorSearch.EF.Tools;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly VectorSearchDbContextFactory _contextFactory;
        private readonly IMathService _mathService;
        public WordService(VectorSearchDbContextFactory contextFactory, IMathService mathService) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _mathService = mathService;
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
            using (var context = _contextFactory.Create())
            {
                var searchWord = await context.Words.FirstOrDefaultAsync(w => w.Text == searchOptions.Text);

                if (searchOptions.Text == null || string.IsNullOrEmpty(searchWord.Vector))
                    return new PagedResult<WordDto>()
                    {
                        Data = new List<WordDto>(),
                        CurrentPage = searchOptions.PageNumber,
                        TotalPages = 0,
                        TotalRecords = 0
                    };

                var targetVector = searchWord.Vector.ParseVector();

                var words = await context.Words.Select(w => new WordDto { Id = w.Id, Text = w.Text, Vector = w.Vector }).ToListAsync();

                var similarWords = words
                    .Select(word =>
                    {
                        word.Similarity = word.Similarity = _mathService.ComputeCosineSimilarity(targetVector, word.Vector.ParseVector());
                        return word;
                    })
                    .OrderByDescending(word => word.Similarity);

                var totalRecords = similarWords.Count();

                var pagedData = similarWords
                    .Skip((searchOptions.PageNumber - 1) * searchOptions.PageSize)
                    .Take(searchOptions.PageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling((double)totalRecords / searchOptions.PageSize);

                return new PagedResult<WordDto>()
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
