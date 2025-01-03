﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class VectorSearchViewModel : ViewModelBase
    {
        private string _errorMessage;
        private bool _isLoading;
        private string _searchText;
        private bool _isVectorSearchEnabled;
        private ObservableCollection<WordDto> _words;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public bool IsVectorSearchEnabled
        {
            get
            {
                return _isVectorSearchEnabled;
            }
            set
            {
                _isVectorSearchEnabled = value;
                OnPropertyChanged(nameof(IsVectorSearchEnabled));
            }
        }
        public ObservableCollection<WordDto> Words
        {
            get
            {
                return _words;
            }
            set
            {
                _words = value;
                OnPropertyChanged(nameof(Words));
            }
        }

        public ICommand SearchCommand { get; set; }

        public VectorSearchViewModel(VectorSearchStore store)
        {
            Words = new ObservableCollection<WordDto>();
            SearchCommand = new LoadWordsCommand(this, store);
        }
    }
}
