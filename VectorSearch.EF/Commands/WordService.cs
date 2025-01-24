using Flurl;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Exceptions;
using VectorSearch.Domain.Models;
using VectorSearch.Domain.ViewModels;
using VectorSearch.EF.Contexts;
using VectorSearch.EF.Tools;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly IMathService _mathService;
        private readonly IDbSetService _dbSetService;
        private readonly VectorSearchOptions _options;
        private readonly IExpressionService _expressionService;
        private readonly IDbContextFactory<VectorSearchDbContext> _contextFactory;
        public WordService(IDbContextFactory<VectorSearchDbContext> contextFactory, IMathService mathService, IExpressionService expressionService, IDbSetService dbSetService, VectorSearchOptions options) : base(contextFactory)
        {
            _options = options;
            _mathService = mathService;
            _dbSetService = dbSetService;
            _contextFactory = contextFactory;
            _expressionService = expressionService;
        }

        public async Task<PagedResult<WordDto>> GetAllAsync(SearchOptions searchOptions)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                IQueryable<IWord> queryable = _dbSetService.GetProperDbSet(searchOptions, context);

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

            using (var context = _contextFactory.CreateDbContext())
            {
                IQueryable<IWord> queryable = _dbSetService.GetProperDbSet(searchOptions, context);

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

        public async Task<List<WordDto>> CompareWords(CompareWordsRequestViewModel request)
        {
            if (string.IsNullOrEmpty(request.FirstWord))
                throw new WordsNotAssignedException("First word is not assigned");

            if (string.IsNullOrEmpty(request.SecondWord))
                throw new WordsNotAssignedException("Second word is not assigned");

            using (var context = _contextFactory.CreateDbContext())
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

                var thirdSearchedWord = WordDto.Empty;

                if(!string.IsNullOrEmpty(request.ThirdWord))
                    thirdSearchedWord = await context.Glove50Ds.Where(w => w.Text == request.ThirdWord).Select(w => new WordDto() { Text = w.Text, Vector = w.Vector }).FirstOrDefaultAsync();


                if (!string.IsNullOrEmpty(request.ThirdWord) && string.IsNullOrEmpty(thirdSearchedWord?.Text))
                    throw new WordNotFoundException($"Word {request.ThirdWord} not found in the dictionary");

                var firstVector = new Vector(_mathService.ParseVector(firstSearchedWord.Vector));
                var secondVector = new Vector(_mathService.ParseVector(secondSearchedWord.Vector));
                var thirdVector = string.IsNullOrEmpty(thirdSearchedWord?.Text) ? new Vector() : new Vector(_mathService.ParseVector(thirdSearchedWord.Vector));

                var finalVector = _expressionService.CalculateVector(new CalculateVectorRequest()
                {
                    FirstVector = firstVector,
                    FirstOperation = request.FirstOperation,
                    SecondVector = secondVector,
                    SecondOperation = request.SecondOperation,
                    ThirdVector = thirdVector,
                });

                var excludedWords = new List<string>()
                {
                    request.FirstWord,
                    request.SecondWord,
                    request.ThirdWord ?? string.Empty,
                };

                var words = await context.Glove50Ds
                    .AsNoTracking()
                    .Where(w => !string.IsNullOrEmpty(w.Vector) && !excludedWords.Contains(w.Text))
                    .Select(w => new { w.Id, w.Text, w.Vector })
                    .ToListAsync();


                var similarWords = new ConcurrentBag<WordDto>();
                Parallel.ForEach(words, word =>
                {
                    var wordVector = _mathService.ParseVector(word.Vector);
                    var similarity = _mathService.ComputeCosineSimilarity(finalVector.Elements, wordVector);

                    similarWords.Add(new WordDto
                    {
                        Id = word.Id,
                        Text = word.Text,
                        Vector = word.Vector,
                        Similarity = similarity
                    });
                });


                var orderedWords = similarWords.OrderByDescending(word => word.Similarity).Take(Convert.ToInt16(_options.TakeNearesWords)).ToList();

                return orderedWords;
            }
        }

        public async Task<(string, List<SimplifiedDictionaryResultItem>)> GetWordMeaningFromDictionary(string word)
        {
            var response = await _options.DictioanryUri
                    .AppendPathSegment(word)
                    .GetJsonAsync<List<DictionaryResultViewModelItem>>();

            string? phonetic = (from r in response select r.Phonetic).FirstOrDefault();

            var simplifiedResult = response
                .SelectMany(result => result.Meanings, (result, meaning) => new
                {
                    result.Phonetic,
                    meaning.PartOfSpeech,
                    Definitions = meaning.Definitions.Select(d => d.Definition)
                })
                .SelectMany(item => item.Definitions.Select(definition => new SimplifiedDictionaryResultItem()
                {
                    PartOfSpeech = item.PartOfSpeech,
                    Definition = definition
                }))
                .OrderBy(x => x.PartOfSpeech)
                .ToList();

            return (phonetic, simplifiedResult);
        }
    }
}
