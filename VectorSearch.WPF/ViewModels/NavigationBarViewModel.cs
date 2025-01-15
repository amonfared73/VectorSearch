using System.Windows.Input;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Services;

namespace VectorSearch.WPF.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateAboutCommand { get; set; }
        public NavigationBarViewModel(NavigationService<VectorSearchViewModel> homeNavigationService, NavigationService<AboutViewModel> aboutNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<VectorSearchViewModel>(homeNavigationService);
            NavigateAboutCommand = new NavigateCommand<AboutViewModel>(aboutNavigationService);
        }
    }
}
