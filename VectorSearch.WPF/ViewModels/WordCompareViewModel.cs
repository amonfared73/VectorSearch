using System.Windows.Input;
using VectorSearch.Domain.Enums;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordCompareViewModel : ViewModelBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly IDialougeService _dialougeService;

        private string _firstWord;
        private string _secondWord;
        private WordCompareOperationType _wordCompareOperationType;

        public WordCompareViewModel(VectorSearchStore vectorSearchStore, IDialougeService dialougeService)
        {
            _vectorSearchStore = vectorSearchStore;
            _dialougeService = dialougeService;
        }

        public string FirstWord
        {
            get { return _firstWord; }
            set
            {
                _firstWord = value;
                OnPropertyChanged(nameof(FirstWord));
            }
        }
        public string SecondWord
        {
            get
            {
                return _secondWord;
            }
            set
            {
                _secondWord = value;
                OnPropertyChanged(nameof(SecondWord));
            }
        }
        public WordCompareOperationType WordCompareOperationType
        {
            get
            {
                return _wordCompareOperationType;
            }
            set
            {
                _wordCompareOperationType = value;
                OnPropertyChanged(nameof(WordCompareOperationType));
            }
        }

        public ICommand CompareCommand { get; set; }
    }
}
