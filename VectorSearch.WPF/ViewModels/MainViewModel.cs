using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchStore _vectorSearchStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainViewModel(VectorSearchStore vectorSearchStore, NavigationStore navigationStore)
        {
            _vectorSearchStore = vectorSearchStore;
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
