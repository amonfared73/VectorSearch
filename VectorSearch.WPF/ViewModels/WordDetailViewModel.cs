﻿using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordDetailViewModel : ViewModelBase
    {
        private readonly SelectedWordStore _selectedWordStore;
        public WordDto SelectedWord => _selectedWordStore.SelectedWord;
        public WordDetailViewModel(SelectedWordStore selectedWordStore)
        {
            _selectedWordStore = selectedWordStore;
            _selectedWordStore.SelectedWordChanged += OnSelectedWordChanged;
        }

        private void OnSelectedWordChanged()
        {
            OnPropertyChanged(nameof(Word));
            OnPropertyChanged(nameof(Similarity));
            OnPropertyChanged(nameof(Vector));
        }

        public string Word => SelectedWord?.Text != null ? SelectedWord.Text : "Unassigned";
        public double Similarity => SelectedWord?.Similarity != null ? SelectedWord.Similarity : 0.00;
        public string Vector => SelectedWord?.Vector != null ? SelectedWord.Vector : "No Vector found!";
        
    }
}
