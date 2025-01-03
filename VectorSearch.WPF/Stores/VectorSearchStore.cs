using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task Load(string searchText)
        {
            IEnumerable<WordDto> words = await _wordService.GetAllAsync(searchText);
            _words.Clear();
            _words.AddRange(words);
            WordsLoaded?.Invoke();
        }
    }
}
