using System.Windows;
using VectorSearch.Domain.Configurations;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Components
{
    /// <summary>
    /// Interaction logic for DialougeBox.xaml
    /// </summary>
    public partial class DialougeBox : Window
    {
        public DialougeBox(DialougeBoxOptions options)
        {
            InitializeComponent();
            DataContext = new DialogBoxViewModel()
            {
                Title = options.Title,
                Message = options.Message,
                CloseText = options.CloseText
            };
        }
    }
}
