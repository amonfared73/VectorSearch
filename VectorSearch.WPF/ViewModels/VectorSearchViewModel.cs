using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class VectorSearchViewModel : ViewModelBase
    {
        private string _errorMessage;
        private bool _isLoading;
        private string _searchText;
        private bool _isVectorSearchEnabled;
        private ObservableCollection<WordDto> _words;
        private readonly VectorSearchStore _store;
        private int _currentPage;
        private int _totalPages;
        private int? _totalRecords;
        private string? _paginationInfo;
        private GloveType _gloveType;

        public GloveType GloveType
        {
            get { return _gloveType; }
            set { if (_gloveType != value) { _gloveType = value; OnPropertyChanged(nameof(GloveType)); } }
        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set { if (_currentPage != value) { _currentPage = value; OnPropertyChanged(nameof(CurrentPage)); OnPropertyChanged(nameof(Words)); OnPropertyChanged(nameof(PaginationInfo)); } }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; OnPropertyChanged(nameof(TotalPages)); OnPropertyChanged(nameof(PaginationInfo)); }
        }
        public int? TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; OnPropertyChanged(nameof(TotalRecords)); OnPropertyChanged(nameof(PaginationInfo)); }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); OnPropertyChanged(nameof(HasErrorMessage)); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { if (_searchText != value) { _searchText = value; OnPropertyChanged(nameof(SearchText)); } }
        }
        public bool IsVectorSearchEnabled
        {
            get { return _isVectorSearchEnabled; }
            set { _isVectorSearchEnabled = value; OnPropertyChanged(nameof(IsVectorSearchEnabled)); }
        }
        public ObservableCollection<WordDto> Words
        {
            get { return _words; }
            set { _words = value; OnPropertyChanged(nameof(Words)); }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public string? PaginationInfo => $"PageNumber: {CurrentPage}, TotalPages: {TotalPages}, TotalRecords: {TotalRecords}";

        public VectorSearchViewModel(VectorSearchStore store)
        {
            _store = store;
            CurrentPage = 1;
            GloveType = GloveType.glove_6B_50d;
            Words = new ObservableCollection<WordDto>();
            SearchCommand = new LoadWordsCommand(this, _store);
            PreviousPageCommand = new PreviousPageCommand(this, _store);
            NextPageCommand = new NextPageCommand(this, _store);
            _store.WordsLoaded += OnWordsLoaded;
        }

        private void OnWordsLoaded()
        {
            _words.Clear();
            foreach (var word in _store.PagedWords.Data)
            {
                AddWord(word);
            }
            CurrentPage = _store.PagedWords.CurrentPage;
            TotalPages = _store.PagedWords.TotalPages;
            TotalRecords = _store.PagedWords.TotalRecords;
        }

        private void AddWord(WordDto word)
        {
            var item = new WordDto() { Id = word.Id, Text = word.Text, Vector = word.Vector, Similarity = Math.Round(word.Similarity * 100, 2) };
            _words.Add(item);
        }
    }
}
