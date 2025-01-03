using Microsoft.EntityFrameworkCore;
using System.Windows;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.EF.Commands;
using VectorSearch.EF.Contexts;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IWordService _service;
        private readonly VectorSearchStore _store;
        private readonly VectorSearchDbContextFactory _contextFactory;

        public App()
        {
            var connectionString = "Server=localhost;Database=VectorSearch;Trusted_Connection=True;TrustServerCertificate=True;";
            _contextFactory = new VectorSearchDbContextFactory(new DbContextOptionsBuilder<VectorSearchDbContext>().UseSqlServer(connectionString).Options);
            _service = new WordService(_contextFactory);
            _store = new VectorSearchStore(_service);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var context = _contextFactory.Create())
            {
                context.Database.Migrate();
            }

            var mainWindow = new MainWindow()
            {
                DataContext = new VectorSearchViewModel(_store)
            };
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
