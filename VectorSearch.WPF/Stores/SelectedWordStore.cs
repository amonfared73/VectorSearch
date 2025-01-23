using VectorSearch.Domain.DTOs;

namespace VectorSearch.WPF.Stores
{
    public class SelectedWordStore
    {
        private WordDto _selectedWord;
        public WordDto SelectedWord
        {
            get
            {
                return _selectedWord;
            }
            set
            {
                _selectedWord = value;
                SelectedWordChanged?.Invoke();
            }
        }
        public event Action SelectedWordChanged;

        private readonly VectorSearchStore _vectorSearchStore;
        public SelectedWordStore(VectorSearchStore vectorSearchStore)
        {
            _vectorSearchStore = vectorSearchStore;
        }
    }
}
