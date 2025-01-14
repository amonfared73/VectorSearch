using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class NavigateAboutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchStore _vectorSearchStore;

        public NavigateAboutCommand(NavigationStore navigationStore, VectorSearchStore vectorSearchStore)
        {
            _navigationStore = navigationStore;
            _vectorSearchStore = vectorSearchStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new AboutViewModel(_navigationStore, _vectorSearchStore);
        }
    }
}
