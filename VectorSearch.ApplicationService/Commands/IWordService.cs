﻿using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using VectorSearch.Domain.ViewModels;

namespace VectorSearch.ApplicationService.Commands
{
    public interface IWordService : IBaseService<Word>
    {
        Task<PagedResult<WordDto>> GetAllAsync(SearchOptions searchOptions);
        Task<PagedResult<WordDto>> GetAllSimilarWordsEF(SearchOptions searchOptions);
        Task<PagedResult<WordDto>> ComplexSemanticSearch(SearchOptions searchOptions);
        Task<List<WordDto>> CompareWords(CompareWordsRequestViewModel request);
        Task<(string, List<SimplifiedDictionaryResultItem>)> GetWordMeaningFromDictionary(string word);
    }
}
