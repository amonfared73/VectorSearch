using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.ViewModels;

namespace VectorSearch.WPF.Stores
{
    public class DictionaryStore
    {
        public string Phonetic;
        private readonly IWordService _wordService;
        private readonly List<SimplifiedDictionaryResultItem> _dictionaryResultItems;

        public List<SimplifiedDictionaryResultItem> DictionaryResultItems => _dictionaryResultItems;
        public event Action ResultsLoaded;

        public DictionaryStore(IWordService wordService)
        {
            _wordService = wordService;
            _dictionaryResultItems = new List<SimplifiedDictionaryResultItem>();
        }

        public async Task Load(string word)
        {
            (string phonetic, List<SimplifiedDictionaryResultItem> results) = await _wordService.GetWordMeaningFromDictionary(word);
            Phonetic = phonetic;
            _dictionaryResultItems.Clear();
            _dictionaryResultItems.AddRange(results);
            ResultsLoaded?.Invoke();
        }

    }
}
