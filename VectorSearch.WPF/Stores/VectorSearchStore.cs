using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Models;

namespace VectorSearch.WPF.Stores
{
    public class VectorSearchStore
    {
        private readonly IWordService _wordService;
        private readonly List<Word> _words;
        public List<Word> Words => _words;

        public event Action WordsLoaded;
        public VectorSearchStore(IWordService wordService)
        {
            _wordService = wordService;
            _words = new List<Word>();
        }

        public async Task Load()
        {
            IEnumerable<Word> words = await _wordService.Execute();
            _words.Clear();
            _words.AddRange(words);
            WordsLoaded?.Invoke();
        }
    }
}
