using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewmodel;

        public NavigationService(NavigationStore navigationStore, VectorSearchStore vectorSearchStore, Func<TViewModel> createViewmodel)
        {
            _navigationStore = navigationStore;
            _createViewmodel = createViewmodel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewmodel();
        }
    }
}
