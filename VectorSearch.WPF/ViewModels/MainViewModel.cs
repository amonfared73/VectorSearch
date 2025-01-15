using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchStore _vectorSearchStore;
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainViewModel(VectorSearchStore vectorSearchStore, NavigationStore navigationStore, NavigationBarViewModel navigationBarViewModel)
        {
            _vectorSearchStore = vectorSearchStore;
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            NavigationBarViewModel = navigationBarViewModel;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
