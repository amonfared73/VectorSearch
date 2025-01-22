using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordDetailViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedWordStore _selectedWordStore;
        private readonly Func<VectorSearchViewModel> _createVectorSearchViewmodel;
        public WordDetailViewModel(ModalNavigationStore modalNavigationStore, SelectedWordStore selectedWordStore, Func<VectorSearchViewModel> createVectorSearchViewmodel)
        {
            _modalNavigationStore = modalNavigationStore;
            _selectedWordStore = selectedWordStore;
            _createVectorSearchViewmodel = createVectorSearchViewmodel;
        }

        private string _word;
        public string Word
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
                _selectedWordStore.SelectedWord = _createVectorSearchViewmodel()?.SelectedWord;
                OnPropertyChanged(nameof(Word));    
            }
        }

        private double _similarity;
        public double Similarity
        {
            get
            {
                return _similarity;
            }
            set
            {
                _similarity = value;
                OnPropertyChanged(nameof(Similarity));
            }
        }



        private string _vector;
        public string Vector
        {
            get
            {
                return _vector;
            }
            set
            {
                _vector = value;
                OnPropertyChanged(nameof(Vector));
            }
        }
    }
}
