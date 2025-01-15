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
using VectorSearch.WPF.Services;

namespace VectorSearch.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IMathService _mathService;
        private readonly IWordService _wordService;
        private readonly IDialougeService _dialougeService;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchDbContextFactory _contextFactory;
        private readonly NavigationBarViewModel _navigationBarViewModel;

        public VectorSearchOptions Options { get; set; }
        public App()
        {
            LoadConfigurations();
            _contextFactory = new VectorSearchDbContextFactory(new DbContextOptionsBuilder<VectorSearchDbContext>().UseSqlServer(Options.ConnectionString).Options);
            _mathService = new MathService();
            _dialougeService = new DialougeService();
            _wordService = new WordService(_contextFactory, _mathService, Options);
            _vectorSearchStore = new VectorSearchStore(_wordService);
            _navigationStore = new NavigationStore();
            _navigationBarViewModel = new NavigationBarViewModel(CreateHomeNavigationService(), CreateAboutNavigationService(), _navigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationService<VectorSearchViewModel> homeNavigationService = CreateHomeNavigationService();
            homeNavigationService.Navigate();
            var mainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_vectorSearchStore, _navigationStore, _navigationBarViewModel)
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

        private NavigationService<VectorSearchViewModel> CreateHomeNavigationService()
        {
            return new NavigationService<VectorSearchViewModel>(_navigationStore, _vectorSearchStore, () => new VectorSearchViewModel(_vectorSearchStore, _dialougeService, CreateAboutNavigationService()));
        }

        private NavigationService<AboutViewModel> CreateAboutNavigationService()
        {
            return new NavigationService<AboutViewModel>(_navigationStore, _vectorSearchStore, () => new AboutViewModel(_vectorSearchStore, _dialougeService, CreateHomeNavigationService()));
        }
    }

}
