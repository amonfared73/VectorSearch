using System.Windows;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Components
{
    /// <summary>
    /// Interaction logic for DialougeBox.xaml
    /// </summary>
    public partial class DialougeBox : Window
    {
        public DialougeBox(string title = "Error", string message = "Some error occured", string closeText = "OK")
        {
            InitializeComponent();
            DataContext = new DialogBoxViewModel()
            {
                Title = title,
                Message = message,
                CloseText = closeText
            };
        }
    }
}
