﻿using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class OpenWordDetailCommand : CommandBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly SelectedWordStore _selectedWordStore;
        private readonly IDialougeService _dialougeService;
        public OpenWordDetailCommand(VectorSearchViewModel vectorSearchViewModel, ModalNavigationStore modalNavigationStore, VectorSearchStore vectorSearchStore, SelectedWordStore selectedWordStore, IDialougeService dialougeService)
        {
            _vectorSearchStore = vectorSearchStore;
            _modalNavigationStore = modalNavigationStore;
            _vectorSearchViewModel = vectorSearchViewModel;
            _selectedWordStore = selectedWordStore;
            _dialougeService = dialougeService;
        }
        public override void Execute(object? parameter)
        {
            WordDto selectedWord = _vectorSearchViewModel.SelectedWord;
            WordDetailViewModel wordDetailViewModel = new WordDetailViewModel(_modalNavigationStore, _selectedWordStore, () => new VectorSearchViewModel(_vectorSearchStore, _modalNavigationStore, _selectedWordStore, _dialougeService))
            {
                Word = selectedWord.Text,
                Similarity = selectedWord.Similarity,
                Vector = selectedWord.Vector,
            };
            _modalNavigationStore.CurrentViewModel = wordDetailViewModel;
        }
    }
}
