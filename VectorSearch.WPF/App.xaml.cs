using System.Configuration;
using System.Data;
using System.Windows;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow()
            {
                DataContext = new VectorSearchViewModel()
            };
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
