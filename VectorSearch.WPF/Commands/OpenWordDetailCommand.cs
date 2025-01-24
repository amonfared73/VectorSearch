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
        private readonly DictionaryStore _dictionaryStore;
        private readonly SelectedWordStore _selectedWordStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchOptions _vectorSearchOptions;
        public OpenWordDetailCommand(VectorSearchViewModel vectorSearchViewModel, ModalNavigationStore modalNavigationStore, SelectedWordStore selectedWordStore, IDialougeService dialougeService, VectorSearchOptions vectorSearchOptions, DictionaryStore dictionaryStore)
        {
            _dialougeService = dialougeService;
            _selectedWordStore = selectedWordStore;
            _modalNavigationStore = modalNavigationStore;
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchOptions = vectorSearchOptions;
            _dictionaryStore = dictionaryStore;
        }
        public override void Execute(object? parameter)
        {
            WordDto selectedWord = _vectorSearchViewModel.SelectedWord;
            WordDetailViewModel wordDetailViewModel = WordDetailViewModel.LoadViewModel(_selectedWordStore, _modalNavigationStore, _vectorSearchOptions, _dialougeService, _dictionaryStore);
            _modalNavigationStore.CurrentViewModel = wordDetailViewModel;
        }
    }
}
