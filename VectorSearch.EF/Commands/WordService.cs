using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.Domain.Exceptions;
using VectorSearch.Domain.Models;
using VectorSearch.Domain.ViewModels;
using VectorSearch.EF.Contexts;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly IMathService _mathService;
        private readonly VectorSearchOptions _options;
        private readonly IExpressionService _expressionService;
        private readonly VectorSearchDbContextFactory _contextFactory;
        public WordService(VectorSearchDbContextFactory contextFactory, IMathService mathService, VectorSearchOptions options, IExpressionService expressionService) : base(contextFactory)
        {
            _options = options;
            _mathService = mathService;
            _contextFactory = contextFactory;
            _expressionService = expressionService;
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


        public async Task<PagedResult<WordDto>> GetAllSimilarWordsEF(SearchOptions searchOptions)
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

                var targetVector = _mathService.ParseVector(searchWord.Vector);

                var words = await queryable
                    .AsNoTracking()
                    .Where(w => !string.IsNullOrEmpty(w.Vector))
                    .Select(w => new { w.Id, w.Text, w.Vector })
                    .ToListAsync();


                var similarWords = new ConcurrentBag<WordDto>();
                Parallel.ForEach(words, word =>
                {
                    var wordVector = _mathService.ParseVector(word.Vector);
                    var similarity = _mathService.ComputeCosineSimilarity(targetVector, wordVector);

                    if (similarity > Convert.ToDouble(_options.SimilarityThreshold))
                    {
                        similarWords.Add(new WordDto
                        {
                            Id = word.Id,
                            Text = word.Text,
                            Vector = word.Vector,
                            Similarity = similarity
                        });
                    }

                });


                var orderedWords = similarWords.OrderByDescending(word => word.Similarity).ToList();

                var totalRecords = orderedWords.Count();

                var pagedData = orderedWords
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

        public async Task<List<WordDto>> CompareWords(CompareWordsRequestViewModel request)
        {
            if (string.IsNullOrEmpty(request.FirstWord))
                throw new WordsNotAssignedException("First word is not assigned");

            if (string.IsNullOrEmpty(request.SecondWord))
                throw new WordsNotAssignedException("Second word is not assigned");

            using (var context = _contextFactory.Create())
            {
                var firstSearchedWord =
                    await context
                    .Glove50Ds
                    .Where(w => w.Text == request.FirstWord)
                    .Select(w => new WordDto() { Text = w.Text, Vector = w.Vector })
                    .FirstOrDefaultAsync();

                if (firstSearchedWord == null)
                    throw new WordNotFoundException($"Word {request.FirstWord} not found in the dictionary");

                var secondSearchedWord =
                    await context
                    .Glove50Ds
                    .Where(w => w.Text == request.SecondWord)
                    .Select(w => new WordDto() { Text = w.Text, Vector = w.Vector })
                    .FirstOrDefaultAsync();

                if (secondSearchedWord == null)
                    throw new WordNotFoundException($"Word {request.SecondWord} not found in the dictionary");

                var thirdSearchedWord =
                    await context
                    .Glove50Ds
                    .Where(w => w.Text == request.ThirdWord)
                    .Select(w => new WordDto() { Text = w.Text, Vector = w.Vector })
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(request.ThirdWord) && thirdSearchedWord == null)
                    throw new WordNotFoundException($"Word {request.ThirdWord} not found in the dictionary");

                var firstVector = new Vector(_mathService.ParseVector(firstSearchedWord.Vector));
                var secondVector = new Vector(_mathService.ParseVector(secondSearchedWord.Vector));
                var thirdVector = new Vector(_mathService.ParseVector(thirdSearchedWord.Vector));

                var finalVector = _expressionService.CalculateVector(new CalculateVectorRequest()
                {
                    FirstVector = firstVector,
                    FirstOperation = request.FirstOperation,
                    SecondVector = secondVector,
                    SecondOperation = request.SecondOperation,
                    ThirdVector = thirdVector,
                });

                var words = await context.Glove50Ds
                    .AsNoTracking()
                    .Where(w => !string.IsNullOrEmpty(w.Vector))
                    .Select(w => new { w.Id, w.Text, w.Vector })
                    .ToListAsync();


                var similarWords = new ConcurrentBag<WordDto>();
                Parallel.ForEach(words, word =>
                {
                    var wordVector = _mathService.ParseVector(word.Vector);
                    var similarity = _mathService.ComputeCosineSimilarity(finalVector.Elements, wordVector);

                    if (similarity > Convert.ToDouble(_options.SimilarityThreshold))
                    {
                        similarWords.Add(new WordDto
                        {
                            Id = word.Id,
                            Text = word.Text,
                            Vector = word.Vector,
                            Similarity = similarity
                        });
                    }

                });


                var orderedWords = similarWords.OrderByDescending(word => word.Similarity).Take(Convert.ToInt16(_options.TakeNearesWords)).ToList();

                return orderedWords;
            }
        }
    }
}
