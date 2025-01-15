using System.Windows.Input;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        public string Title => "GloVe Word Search Application";
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ICommand NavigateHomeCommand { get; set; }
        public AboutViewModel(NavigationStore navigationStore, VectorSearchStore vectorSearchStore, IDialougeService dialougeService, NavigationBarViewModel navigationBarViewModel)
        {
            _vectorSearchStore = vectorSearchStore;
            NavigateHomeCommand = new NavigateCommand<VectorSearchViewModel>(new NavigationService<VectorSearchViewModel>(navigationStore, _vectorSearchStore, () => new VectorSearchViewModel(navigationStore, _vectorSearchStore, dialougeService, navigationBarViewModel)));
            NavigationBarViewModel = navigationBarViewModel;
        }
    }
}
