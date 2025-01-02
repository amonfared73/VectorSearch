using System.Collections.ObjectModel;
using System.Windows.Input;
using VectorSearch.WPF.DTOs;

namespace VectorSearch.WPF.ViewModels
{
    public class VectorSearchViewModel : ViewModelBase
    {
        private string _searchText;
        private bool _searchTypeCode;
        private ObservableCollection<WordDto> _words;

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
        public bool SearchTypeCode
        {
            get
            {
                return _searchTypeCode;
            }
            set
            {
                _searchTypeCode = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand;

        public VectorSearchViewModel(ICommand searchCommand)
        {
            Words = new ObservableCollection<WordDto>();
            SearchCommand = searchCommand;
        }
    }
}
