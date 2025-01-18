using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;
using VectorSearch.Domain.DTOs;

namespace VectorSearch.WPF.Commands
{
    public class CompareWordsCommand : AsyncCommandBase
    {
        private readonly IDialougeService _dialougeService;
        private readonly CompareWordsStore _compareWordsStore;
        private readonly WordCompareViewModel _wordCompareViewModel;

        public CompareWordsCommand(IDialougeService dialougeService, CompareWordsStore compareWordsStore, WordCompareViewModel wordCompareViewModel)
        {
            _dialougeService = dialougeService;
            _compareWordsStore = compareWordsStore;
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
                await _compareWordsStore.Load(new CompareWordsRequestViewModel()
                {
                    FirstWord = _wordCompareViewModel.FirstWord,
                    FirstOperation = _wordCompareViewModel.FirstOperation,
                    SecondWord = _wordCompareViewModel.SecondWord,
                    SecondOperation = _wordCompareViewModel.SecondOperation,
                    ThirdWord = _wordCompareViewModel.ThirdWord,    
                });
            }
            catch (Exception ex)
            {
                _dialougeService.ShowDialouge(options =>
                {
                    options.Title = "Error";
                    options.Message = ex.Message;
                    options.CloseText = "Close";
                });
            }
            finally
            {
                _wordCompareViewModel.IsLoading = false;
            }
        }
    }
}
