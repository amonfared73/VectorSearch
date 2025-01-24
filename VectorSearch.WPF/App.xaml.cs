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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace VectorSearch.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            var vectorSearchOptions = new VectorSearchOptions();
            configuration.GetSection("VectorSearchOptions").Bind(vectorSearchOptions);
            services.AddSingleton(vectorSearchOptions);

            services.AddSingleton<VectorSearchStore>();
            services.AddSingleton<CompareWordsStore>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();
            services.AddSingleton<SelectedWordStore>();
            services.AddSingleton<DictionaryStore>();

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });


            services.AddDbContextFactory<VectorSearchDbContext>(options =>
            {
                options.UseSqlServer(vectorSearchOptions.ConnectionString);
            });

            services.AddSingleton<IMathService, MathService>();
            services.AddScoped<IDbSetService, DbSetService>();
            services.AddSingleton<IDialougeService, DialougeService>();
            services.AddSingleton<IExpressionService, ExpressionService>();
            services.AddScoped<IWordService, WordService>();

            
            
            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<VectorSearchViewModel>
                (
                    serviceProvider.GetRequiredService<NavigationStore>(),
                    () => CreateNavigationbarViewModel(serviceProvider),
                    () => new VectorSearchViewModel
                    (
                        serviceProvider.GetRequiredService<VectorSearchStore>(),
                        serviceProvider.GetRequiredService<ModalNavigationStore>(),
                        serviceProvider.GetRequiredService<SelectedWordStore>(),
                        serviceProvider.GetRequiredService<DictionaryStore>(),
                        serviceProvider.GetRequiredService<IDialougeService>(),
                        serviceProvider.GetRequiredService<VectorSearchOptions>()
                    )
                );
        }

        private INavigationService CreateWordCompareNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<WordCompareViewModel>
                (
                    serviceProvider.GetRequiredService<NavigationStore>(),
                    () => CreateNavigationbarViewModel(serviceProvider),
                    () => new WordCompareViewModel(serviceProvider.GetRequiredService<CompareWordsStore>(), serviceProvider.GetRequiredService<IDialougeService>())
                );
        }

        private INavigationService CreateAboutNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<AboutViewModel>
                (
                    serviceProvider.GetRequiredService<NavigationStore>(),
                    () => CreateNavigationbarViewModel(serviceProvider),
                    () => new AboutViewModel()
                );
        }

        private INavigationService CreateWordDetailModalNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<WordDetailViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => new WordDetailViewModel
                (
                    serviceProvider.GetRequiredService<SelectedWordStore>(),
                    serviceProvider.GetRequiredService<ModalNavigationStore>(),
                    serviceProvider.GetRequiredService<DictionaryStore>(),
                    serviceProvider.GetRequiredService<VectorSearchOptions>(),
                    serviceProvider.GetRequiredService<IDialougeService>())
                );
        }

        private NavigationBarViewModel CreateNavigationbarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel
                (
                    CreateHomeNavigationService(serviceProvider),
                    CreateWordCompareNavigationService(serviceProvider),
                    CreateAboutNavigationService(serviceProvider),
                    serviceProvider.GetRequiredService<NavigationStore>()
                );
        }
    }

}
