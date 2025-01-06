using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;

namespace VectorSearch.WPF.Stores
{
    public class VectorSearchStore
    {
        private readonly IWordService _wordService;
        private readonly List<WordDto> _words;
        public List<WordDto> Words => _words;

        public event Action WordsLoaded;
        public VectorSearchStore(IWordService wordService)
        {
            _wordService = wordService;
            _words = new List<WordDto>();
        }

        public async Task Load(SearchOptions options)
        {
            Func<SearchOptions, Task<IEnumerable<WordDto>>> searchMethod = options.IsVectorSearchEnabled ? _wordService.GetAllSimilarWords : _wordService.GetAllAsync;
            IEnumerable<WordDto> words = await searchMethod(options);
            _words.Clear();
            _words.AddRange(words);
            WordsLoaded?.Invoke();
        }
    }
}
