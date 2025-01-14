using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly Func<TViewModel> _createViewmodel;

        public NavigateCommand(NavigationStore navigationStore, VectorSearchStore vectorSearchStore, Func<TViewModel> createViewmodel)
        {
            _navigationStore = navigationStore;
            _vectorSearchStore = vectorSearchStore;
            _createViewmodel = createViewmodel;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = _createViewmodel();
        }
    }
}
