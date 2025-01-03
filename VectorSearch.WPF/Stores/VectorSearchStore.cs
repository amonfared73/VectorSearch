﻿using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;

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

        public async Task Load()
        {
            IEnumerable<WordDto> words = await _wordService.GetAllAsync();
            _words.Clear();
            _words.AddRange(words);
            WordsLoaded?.Invoke();
        }
    }
}
