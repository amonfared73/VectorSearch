using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class CompareWordsCommand : AsyncCommandBase
    {
        private readonly IDialougeService _dialougeService;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly WordCompareViewModel _wordCompareViewModel;

        public CompareWordsCommand(IDialougeService dialougeService, VectorSearchStore vectorSearchStore, WordCompareViewModel wordCompareViewModel)
        {
            _dialougeService = dialougeService;
            _vectorSearchStore = vectorSearchStore;
            _wordCompareViewModel = wordCompareViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _wordCompareViewModel.IsLoading = true;
            try
            {
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _wordCompareViewModel.IsLoading = false;
            }
        }
    }
}
