using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class OpenWordDetailCommand : CommandBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        public OpenWordDetailCommand(VectorSearchViewModel vectorSearchViewModel, ModalNavigationStore modalNavigationStore, VectorSearchStore vectorSearchStore)
        {
            _vectorSearchStore = vectorSearchStore;
            _modalNavigationStore = modalNavigationStore;
            _vectorSearchViewModel = vectorSearchViewModel;
        }
        public override void Execute(object? parameter)
        {
            WordDto selectedWord = _vectorSearchViewModel.SelectedWord;
            WordDetailViewModel wordDetailViewModel = new WordDetailViewModel();
            _modalNavigationStore.CurrentViewModel = wordDetailViewModel;
        }
    }
}
