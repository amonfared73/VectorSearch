using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class NextPageCommand : AsyncCommandBase
    {
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchStore _vectorSearchStore;

        public NextPageCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore)
        {
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchStore = vectorSearchStore;
        }
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && _vectorSearchViewModel.CurrentPage < _vectorSearchViewModel.TotalPages;
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
                    PageNumber = _vectorSearchViewModel.CurrentPage < _vectorSearchViewModel.TotalPages ?  _vectorSearchViewModel.CurrentPage + 1 : _vectorSearchViewModel.TotalPages,
                });
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
