using System.Windows.Input;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        public string Title => "GloVe Word Search Application";
        public ICommand NavigateHomeCommand { get; set; }
        public AboutViewModel(NavigationStore navigationStore, VectorSearchStore vectorSearchStore)
        {
            _vectorSearchStore = vectorSearchStore;
            NavigateHomeCommand = new NavigateCommand<VectorSearchViewModel>(navigationStore, _vectorSearchStore, () => new VectorSearchViewModel(navigationStore, _vectorSearchStore));
        }
    }
}
