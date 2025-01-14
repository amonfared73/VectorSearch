using System.Windows.Input;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        public string Text => "About Page";
        public ICommand NavigateHomeCommand { get; set; }
        public AboutViewModel(NavigationStore navigationStore, VectorSearchStore vectorSearchStore)
        {
            _vectorSearchStore = vectorSearchStore;
            NavigateHomeCommand = new NavigateHomeCommand(navigationStore, _vectorSearchStore);
        }
    }
}
