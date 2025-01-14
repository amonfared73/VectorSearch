using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class NavigateHomeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchStore _vectorSearchStore;
        public NavigateHomeCommand(NavigationStore navigationStore, VectorSearchStore vectorSearchStore)
        {
            _navigationStore = navigationStore;
            _vectorSearchStore = vectorSearchStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new VectorSearchViewModel(_navigationStore, _vectorSearchStore);
        }
    }
}
