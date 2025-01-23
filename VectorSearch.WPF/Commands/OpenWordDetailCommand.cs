using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class OpenWordDetailCommand : CommandBase
    {
        private readonly IDialougeService _dialougeService;
        private readonly SelectedWordStore _selectedWordStore;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchOptions _vectorSearchOptions;
        public OpenWordDetailCommand(VectorSearchViewModel vectorSearchViewModel, ModalNavigationStore modalNavigationStore, VectorSearchStore vectorSearchStore, SelectedWordStore selectedWordStore, IDialougeService dialougeService, VectorSearchOptions vectorSearchOptions)
        {
            _dialougeService = dialougeService;
            _selectedWordStore = selectedWordStore;
            _vectorSearchStore = vectorSearchStore;
            _modalNavigationStore = modalNavigationStore;
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchOptions = vectorSearchOptions;
        }
        public override void Execute(object? parameter)
        {
            WordDto selectedWord = _vectorSearchViewModel.SelectedWord;
            WordDetailViewModel wordDetailViewModel = new WordDetailViewModel(_selectedWordStore, _modalNavigationStore, _vectorSearchOptions, _dialougeService);
            _modalNavigationStore.CurrentViewModel = wordDetailViewModel;
        }
    }
}
