using System.Windows.Input;
using VectorSearch.WPF.Commands;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;

namespace VectorSearch.WPF.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly VectorSearchStore _vectorSearchStore;
        public string Title => "GloVe Word Search Application";
        public AboutViewModel(VectorSearchStore vectorSearchStore, IDialougeService dialougeService)
        {
            _vectorSearchStore = vectorSearchStore;
        }
        ~AboutViewModel()
        {

        }
    }
}
