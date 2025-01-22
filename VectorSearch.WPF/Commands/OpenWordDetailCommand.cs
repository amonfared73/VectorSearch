using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class OpenWordDetailCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchStore _vectorSearchStore;
        public OpenWordDetailCommand(ModalNavigationStore modalNavigationStore, VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchStore = vectorSearchStore;
        }
        public override void Execute(object? parameter)
        {
            WordDto selectedWord = _vectorSearchViewModel.SelectedWord;
            WordDetailViewModel wordDetailViewModel = new WordDetailViewModel();
            _modalNavigationStore.CurrentViewModel = wordDetailViewModel;
        }
    }
}
