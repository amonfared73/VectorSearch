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
        private readonly IDbSetService _dbSetService;
        private readonly IExpressionService _expressionService;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly CompareWordsStore _compareWordsStore;
        private readonly NavigationStore _navigationStore;
        private readonly VectorSearchDbContextFactory _contextFactory;

        public VectorSearchOptions Options { get; set; }
        public App()
        {
            LoadConfigurations();
            _contextFactory = new VectorSearchDbContextFactory(new DbContextOptionsBuilder<VectorSearchDbContext>().UseSqlServer(Options.ConnectionString).Options);
            _mathService = new MathService();
            _dbSetService = new DbSetService();
            _dialougeService = new DialougeService();
            _expressionService = new ExpressionService();
            _wordService = new WordService(_contextFactory, _mathService, _expressionService, _dbSetService, Options);
            _vectorSearchStore = new VectorSearchStore(_wordService);
            _compareWordsStore = new CompareWordsStore(_wordService);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService<VectorSearchViewModel> homeNavigationService = CreateHomeNavigationService();
            homeNavigationService.Navigate();
            var mainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_vectorSearchStore, _navigationStore)
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

        private INavigationService<VectorSearchViewModel> CreateHomeNavigationService()
        {
            return new LayoutNavigationService<VectorSearchViewModel>(_navigationStore, CreateNavigationbarViewModel, () => new VectorSearchViewModel(_vectorSearchStore, _dialougeService));
        }

        private INavigationService<WordCompareViewModel> CreateWordCompareNavigationService()
        {
            return new LayoutNavigationService<WordCompareViewModel>(_navigationStore, CreateNavigationbarViewModel, () => new WordCompareViewModel(_compareWordsStore, _dialougeService));
        }

        private INavigationService<AboutViewModel> CreateAboutNavigationService()
        {
            return new LayoutNavigationService<AboutViewModel>(_navigationStore, CreateNavigationbarViewModel, () => new AboutViewModel(_vectorSearchStore, _dialougeService));
        }

        private NavigationBarViewModel CreateNavigationbarViewModel()
        {
            return new NavigationBarViewModel(CreateHomeNavigationService(), CreateWordCompareNavigationService(), CreateAboutNavigationService(), _navigationStore);
        }
    }

}
