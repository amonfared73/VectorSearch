using System.Windows.Input;

namespace VectorSearch.WPF.ViewModels
{
    public class DialogBoxViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ICommand CloseCommand { get; set; }
        public string CloseText { get; set; }
    }
}
