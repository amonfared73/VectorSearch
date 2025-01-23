using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.ViewModels;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordDetailViewModel : ViewModelBase
    {
        private readonly SelectedWordStore _selectedWordStore;
        private readonly DictionaryStore _dictionaryStore;
        public WordDto SelectedWord => _selectedWordStore.SelectedWord;
        public WordDetailViewModel(SelectedWordStore selectedWordStore, ModalNavigationStore modalNavigationStore, VectorSearchOptions options, IDialougeService dialougeService, DictionaryStore dictionaryStore)
        {
            _selectedWordStore = selectedWordStore;
            _dictionaryStore = dictionaryStore;
            DictionaryResultItems = new ObservableCollection<SimplifiedDictionaryResultItem>();
            _dictionaryStore.ResultsLoaded += OnResulsLoaded;
            _selectedWordStore.SelectedWordChanged += OnSelectedWordChanged;
            LoadWordMeaningCommand = new LoadWordMeaningCommand(this, options, dialougeService, _dictionaryStore);
            CloseCommand = new CloseModalCommand(modalNavigationStore);
        }

        private void OnResulsLoaded()
        {
            _dictionaryResultsItems.Clear();
            foreach (var result in _dictionaryStore.DictionaryResultItems)
            {
                var item = new SimplifiedDictionaryResultItem() { PartOfSpeech = result.PartOfSpeech, Definition = result.Definition };
                _dictionaryResultsItems.Add(item);
            }
            Phonetic = _dictionaryStore.Phonetic;
        }

        private void OnSelectedWordChanged()
        {
            OnPropertyChanged(nameof(Word));
            OnPropertyChanged(nameof(Similarity));
            OnPropertyChanged(nameof(Vector));
        }

        public override void Dispose()
        {
            _selectedWordStore.SelectedWordChanged -= OnSelectedWordChanged;
            _dictionaryStore.ResultsLoaded -= OnResulsLoaded;
            base.Dispose();
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private string _phonetic;
        public string Phonetic
        {
            get
            {
                return _phonetic;
            }
            set
            {
                _phonetic = value;
                OnPropertyChanged(nameof(Phonetic));
            }
        }
        private ObservableCollection<SimplifiedDictionaryResultItem> _dictionaryResultsItems;
        public ObservableCollection<SimplifiedDictionaryResultItem> DictionaryResultItems
        {
            get
            {
                return _dictionaryResultsItems;
            }
            set
            {
                _dictionaryResultsItems = value;
                OnPropertyChanged(nameof(DictionaryResultItems));
            }
        }

        private bool _showResults;
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
        public ICommand LoadWordMeaningCommand {  get; set; }
        public ICommand CloseCommand { get; set; }
        public string Word => SelectedWord?.Text != null ? SelectedWord.Text : "Unassigned";
        public double Similarity => SelectedWord?.Similarity != null ? SelectedWord.Similarity : 0.00;
        public string Vector => SelectedWord?.Vector != null ? SelectedWord.Vector : "No Vector found!";
        public string Meaning { get; set; } = "No Definitions Found";

    }
}
