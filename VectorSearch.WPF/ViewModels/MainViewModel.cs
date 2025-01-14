using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        public ViewModelBase CurrentViewModel { get; }
        public MainViewModel(VectorSearchStore vectorSearchStore)
        {
            _vectorSearchStore = vectorSearchStore;
            CurrentViewModel = new VectorSearchViewModel(_vectorSearchStore);
        }
    }
}
