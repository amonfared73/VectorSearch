using System.Windows.Input;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class WordDetailViewModel : ViewModelBase
    {
        private readonly SelectedWordStore _selectedWordStore;
        private readonly VectorSearchOptions _options;
        public WordDto SelectedWord => _selectedWordStore.SelectedWord;
        public WordDetailViewModel(SelectedWordStore selectedWordStore, ModalNavigationStore modalNavigationStore, VectorSearchOptions options)
        {
            _options = options;
            _selectedWordStore = selectedWordStore;
            _selectedWordStore.SelectedWordChanged += OnSelectedWordChanged;
            CloseCommand = new CloseModalCommand(modalNavigationStore);
        }

        private void OnSelectedWordChanged()
        {
            OnPropertyChanged(nameof(Word));
            OnPropertyChanged(nameof(Similarity));
            OnPropertyChanged(nameof(Vector));
        }

        public override void Dispose()
        {
            _selectedWordStore.SelectedWordChanged -= OnSelectedWordChanged;
            base.Dispose();
        }

        public ICommand CloseCommand { get; set; }
        public string Word => SelectedWord?.Text != null ? SelectedWord.Text : "Unassigned";
        public double Similarity => SelectedWord?.Similarity != null ? SelectedWord.Similarity : 0.00;
        public string Vector => SelectedWord?.Vector != null ? SelectedWord.Vector : "No Vector found!";
        public string Meaning { get; set; } = "No Definitions Found";

    }
}
