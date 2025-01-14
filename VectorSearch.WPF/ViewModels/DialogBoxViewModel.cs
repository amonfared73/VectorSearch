using System.Windows.Input;
using VectorSearch.WPF.Commands;

namespace VectorSearch.WPF.ViewModels
{
    public class DialogBoxViewModel : ViewModelBase
    {
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public ICommand CloseCommand { get; set; }
        public string CloseText { get; set; }
        public DialogBoxViewModel()
        {
            CloseCommand = new CloseDialougeCommand(this);
        }
    }
}
