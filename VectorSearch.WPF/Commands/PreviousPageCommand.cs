using System.Windows;
using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class PreviousPageCommand : AsyncCommandBase
    {
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchStore _vectorSearchStore;

        public PreviousPageCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore)
        {
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchStore = vectorSearchStore;
        }
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            _vectorSearchViewModel.ErrorMessage = null;
            _vectorSearchViewModel.IsLoading = true;
            try
            {
                await _vectorSearchStore.Load(new SearchOptions()
                {
                    Text = _vectorSearchViewModel.SearchText,
                    IsVectorSearchEnabled = _vectorSearchViewModel.IsVectorSearchEnabled,
                    PageNumber = _vectorSearchViewModel.CurrentPage > 1 ? _vectorSearchViewModel.CurrentPage - 1 : 1,
                });
            }
            catch (Exception ex)
            {
                _vectorSearchViewModel.ErrorMessage = ex.Message ?? "Some error occured";
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _vectorSearchViewModel.IsLoading = false;
            }
        }
    }
}
