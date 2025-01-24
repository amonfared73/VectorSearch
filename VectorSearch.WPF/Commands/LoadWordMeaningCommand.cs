using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class LoadWordMeaningCommand : AsyncCommandBase
    {
        private readonly WordDetailViewModel _wordDetailViewModel;
        private readonly DictionaryStore _dictionaryStore;
        public LoadWordMeaningCommand(WordDetailViewModel wordDetailViewModel, DictionaryStore dictionaryStore)
        {
            _wordDetailViewModel = wordDetailViewModel;
            _dictionaryStore = dictionaryStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _wordDetailViewModel.ErrorMessage = null;
            try
            {
                _wordDetailViewModel.IsLoading = true;
                _wordDetailViewModel.ShowResults = false;
                await _dictionaryStore.Load(_wordDetailViewModel.Word);
                _wordDetailViewModel.IsLoading = false;
                _wordDetailViewModel.ShowResults = true;
            }
            catch (Exception ex)
            {
                _wordDetailViewModel.IsLoading = false;
                _wordDetailViewModel.ShowResults = false;
                _wordDetailViewModel.ErrorMessage = "Seems like we couldn't find any definition!";
            }
            finally
            {
                _wordDetailViewModel.IsLoading = false;
            }
        }
    }
}
