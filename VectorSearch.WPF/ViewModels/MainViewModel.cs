﻿using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;
        public MainViewModel(VectorSearchStore vectorSearchStore, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _vectorSearchStore = vectorSearchStore;
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
