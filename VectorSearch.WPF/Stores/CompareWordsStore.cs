using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;

namespace VectorSearch.WPF.Stores
{
    public class CompareWordsStore
    {
        private readonly IWordService _wordService;
        private readonly List<WordDto> _words;
        public List<WordDto> Words => _words;
        public event Action WordsLoaded;
        public CompareWordsStore(IWordService wordService)
        {
            _wordService = wordService;
            _words = new List<WordDto>();
        }

        public async Task Load(CompareWordsRequestViewModel request)
        {
            List<WordDto> words = await _wordService.CompareWords(request);
            _words.Clear();
            _words.AddRange(words);
            WordsLoaded?.Invoke();
        }

    }
}
