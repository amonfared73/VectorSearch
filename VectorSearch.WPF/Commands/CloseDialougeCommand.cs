using System.Windows;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class CloseDialougeCommand : CommandBase
    {
        private readonly DialogBoxViewModel _dialogBoxViewModel;
        public CloseDialougeCommand(DialogBoxViewModel dialogBoxViewModel)
        {
            _dialogBoxViewModel = dialogBoxViewModel;
        }

        public override void Execute(object? parameter)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == _dialogBoxViewModel)?.Close();
        }
    }
}
