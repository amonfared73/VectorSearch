using System.Windows.Input;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateAboutCommand { get; set; }
        public bool IsHomeEnabled => _navigationStore.CurrentViewModel is not VectorSearchViewModel;
        public bool IsAboutEnabled => _navigationStore.CurrentViewModel is not AboutViewModel;
        public NavigationBarViewModel(INavigationService<VectorSearchViewModel> homeNavigationService, INavigationService<AboutViewModel> aboutNavigationService, NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<VectorSearchViewModel>(homeNavigationService);
            NavigateAboutCommand = new NavigateCommand<AboutViewModel>(aboutNavigationService);
            _navigationStore = navigationStore;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
