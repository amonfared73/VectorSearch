using Microsoft.EntityFrameworkCore;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
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
                IQueryable<IWord> queryable = GetProperDbSet(searchOptions, context);

                var query = queryable.AsNoTracking().Where(x => string.IsNullOrEmpty(searchOptions.Text) || x.Text.Contains(searchOptions.Text));

                var totalRecords = await query.CountAsync();

                var data = await query
                    .Skip((searchOptions.PageNumber - 1) * searchOptions.PageSize)
                    .Take(searchOptions.PageSize)
                    .Select(w => new WordDto()
                    {
                        Id = w.Id,
                        Text = w.Text,
                        Vector = w.Vector
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
                IQueryable<IWord> queryable = GetProperDbSet(searchOptions, context);

                var searchWord = await queryable
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

                var words = await queryable
                    .Where(w => !string.IsNullOrEmpty(w.Vector))
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
        private static IQueryable<IWord> GetProperDbSet(SearchOptions searchOptions, VectorSearchDbContext context)
        {
            IQueryable<IWord> queryable;
            switch (searchOptions.GloveType)
            {
                case GloveType.glove_6B_50d:
                    queryable = context.Glove50Ds;
                    break;
                case GloveType.glove_6B_100d:
                    queryable = context.Glove100Ds;
                    break;
                case GloveType.glove_6B_200d:
                    queryable = context.Glove200Ds;
                    break;
                case GloveType.glove_6B_300d:
                    queryable = context.Glove300Ds;
                    break;
                default:
                    throw new ArgumentException("GloveType not assigned");
            }

            return queryable;
        }

    }
}
