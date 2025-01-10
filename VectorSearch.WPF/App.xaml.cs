using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using System.Windows;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.EF.Commands;
using VectorSearch.EF.Contexts;
using VectorSearch.EF.Tools;
using VectorSearch.Domain.Configurations;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IMathService _mathService;
        private readonly IWordService _wordService;
        private readonly VectorSearchStore _store;
        private readonly VectorSearchDbContextFactory _contextFactory;

        public VectorSearchOptions Options { get; set; }
        public App()
        {
            LoadConfigurations();
            _contextFactory = new VectorSearchDbContextFactory(new DbContextOptionsBuilder<VectorSearchDbContext>().UseSqlServer(Options.ConnectionString).Options);
            _mathService = new MathService();
            _wordService = new WordService(_contextFactory, _mathService, Options);
            _store = new VectorSearchStore(_wordService);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //using (var context = _contextFactory.Create())
            //{
            //    context.Database.Migrate();
            //}

            var mainWindow = new MainWindow()
            {
                DataContext = new VectorSearchViewModel(_store)
            };
            mainWindow.Show();
            base.OnStartup(e);
        }

        private void LoadConfigurations()
        {
            var configFilePath = "./../../../appsettings.json";
            var json = File.ReadAllText(configFilePath);
            Options = JsonSerializer.Deserialize<VectorSearchOptions>(json);
        }
    }

}
