using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordCompareViewModel : ViewModelBase
    {
        private bool _showResults;
        private bool _isLoading;
        private string _firstWord;
        private string _secondWord;
        private string _thirdWord;
        private WordCompareOperationType _firstOperation;
        private WordCompareOperationType _secondOperation;
        private string _nearestWord;
        private string _nearestSimilarity;
        private ObservableCollection<WordDto> _words;

        private readonly CompareWordsStore _compareWordsStore;
        private readonly IDialougeService _dialougeService;

        public WordCompareViewModel(CompareWordsStore compareWordsStore, IDialougeService dialougeService)
        {
            _compareWordsStore = compareWordsStore;
            _dialougeService = dialougeService;
            ShowResults = false;
            CompareCommand = new CompareWordsCommand(dialougeService, compareWordsStore, this);
        }
        public ICommand CompareCommand { get; set; }

        public bool ShowResults
        {
            get
            {
                return _showResults;
            }
            set
            {
                _showResults = value;
                OnPropertyChanged(nameof(ShowResults));
            }
        }
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
