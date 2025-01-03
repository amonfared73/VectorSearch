
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class LoadWordsCommand : AsyncCommandBase
    {
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchStore _vectorSearchStore;

        public LoadWordsCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore)
        {
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchStore = vectorSearchStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _vectorSearchViewModel.ErrorMessage = null;
            _vectorSearchViewModel.IsLoading = true;
            try
            {
                await _vectorSearchStore.Load();
            }
            catch (Exception ex)
            {
                _vectorSearchViewModel.ErrorMessage = ex.Message ?? "Some error occured";
            }
            finally
            {
                _vectorSearchViewModel.IsLoading = false;
            }
        }
    }
}
