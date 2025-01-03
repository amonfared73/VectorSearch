using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.WPF.DTOs;

namespace VectorSearch.WPF.ViewModels
{
    public class VectorSearchViewModel : ViewModelBase
    {
        private string _searchText;
        private bool _isVectorSearchEnabled;
        private IEnumerable<WordDto> _words;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        public IEnumerable<WordDto> Words
        {
            get
            {
                return _words;
            }
            set
            {
                _words = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand;

        public VectorSearchViewModel(ICommand searchCommand = null)
        {
            Words = new ObservableCollection<WordDto>();
            SearchCommand = searchCommand;
        }
    }
}
