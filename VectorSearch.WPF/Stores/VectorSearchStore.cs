using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.ViewModels;

namespace VectorSearch.WPF.Stores
{
    public class VectorSearchStore
    {
        private readonly IWordService _wordService;
        private readonly PagedResult<WordDto> _pagedWords;
        public PagedResult<WordDto> PagedWords => _pagedWords;

        public event Action WordsLoaded;
        public VectorSearchStore(IWordService wordService)
        {
            _wordService = wordService;
            _pagedWords = new PagedResult<WordDto>();
        }

        public async Task Load(SearchOptions options)
        {
            Func<SearchOptions, Task<PagedResult<WordDto>>> searchMethod = options.IsVectorSearchEnabled ? _wordService.GetAllSimilarWordsEF : _wordService.GetAllAsync;
            PagedResult<WordDto> pagedWords = await searchMethod(options);
            _pagedWords.Data.Clear();
            _pagedWords.Data.AddRange(pagedWords.Data);
            _pagedWords.CurrentPage = pagedWords.CurrentPage;
            _pagedWords.TotalPages = pagedWords.TotalPages;
            _pagedWords.TotalRecords = pagedWords.TotalRecords;
            WordsLoaded?.Invoke();
        }
    }
}
