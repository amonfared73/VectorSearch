using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.ViewModels;
using VectorSearch.Domain.Enums;

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
            Func<SearchOptions, Task<PagedResult<WordDto>>> searchMethod;

            if (options.IsVectorSearchEnabled)
            {
                switch (options.SourceType)
                {
                    case SourceType.digikala_goods:
                    case SourceType.faranShimi:
                    case SourceType.padidehShimiGharb:
                        searchMethod = _wordService.ComplexSemanticSearch;
                        break;
                    case SourceType.WikipediaFarsi:
                    case SourceType.glove_6B_50d:
                    case SourceType.glove_6B_100d:
                    case SourceType.glove_6B_200d:
                    case SourceType.glove_6B_300d:
                        searchMethod = _wordService.GetAllSimilarWordsEF;
                        break;
                    default:
                        throw new NotSupportedException($"Vector search is not supported for SourceType: {options.SourceType}");
                }
            }
            else
            {
                searchMethod = _wordService.GetAllAsync;
            }


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
