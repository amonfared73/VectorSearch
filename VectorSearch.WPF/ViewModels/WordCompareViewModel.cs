using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordCompareViewModel : ViewModelBase
    {
        private bool _isLoading;
        private string _firstWord;
        private string _secondWord;
        private string _thirdWord;
        private WordCompareOperationType _firstOperation;
        private WordCompareOperationType _secondOperation;
        private string _nearestWord;
        private string _nearestSimilarity;
        private ObservableCollection<WordDto> _words;

        private readonly VectorSearchStore _vectorSearchStore;
        private readonly IDialougeService _dialougeService;

        public WordCompareViewModel(VectorSearchStore vectorSearchStore, IDialougeService dialougeService)
        {
            _vectorSearchStore = vectorSearchStore;
            _dialougeService = dialougeService;
        }
        public ICommand CompareCommand { get; set; }
        public bool IsLoading
        {
            get 
            { 
                return _isLoading; 
            }
            set 
            { 
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading)); 
            }
        }
        public string FirstWord
        {
            get
            {
                return _firstWord;
            }
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
        public string ThirdWord
        {
            get
            {
                return _thirdWord;
            }
            set
            {
                _thirdWord = value;
                OnPropertyChanged(nameof(ThirdWord));
            }
        }
        public WordCompareOperationType FirstOperation
        {
            get
            {
                return _firstOperation;
            }
            set
            {
                _firstOperation = value;
                OnPropertyChanged(nameof(FirstOperation));
            }
        }
        public WordCompareOperationType SecondOperation
        {
            get
            {
                return _secondOperation;
            }
            set
            {
                _secondOperation = value;
                OnPropertyChanged(nameof(SecondOperation));
            }
        }
        public string NearestWord
        {
            get
            {
                return _nearestWord;
            }
            set
            {
                _nearestWord = value;
                OnPropertyChanged(nameof(NearestWord));
            }
        }
        public string NearestSimilarity
        {
            get
            {
                return _nearestSimilarity;
            }
            set
            {
                _nearestSimilarity = value;
                OnPropertyChanged(nameof(NearestSimilarity));
            }
        }
        public ObservableCollection<WordDto> Words
        {
            get
            {
                return _words;
            }
            set
            {
                _words = value;
                OnPropertyChanged(nameof(Words));
            }
        }
    }
}
