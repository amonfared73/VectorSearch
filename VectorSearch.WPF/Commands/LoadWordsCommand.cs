
using System.Windows;
using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class LoadWordsCommand : AsyncCommandBase
    {
        private readonly VectorSearchViewModel _vectorSearchViewModel;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly IDialougeService _dialougeService;

        public LoadWordsCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore, IDialougeService dialougeService)
        {
            _vectorSearchViewModel = vectorSearchViewModel;
            _vectorSearchStore = vectorSearchStore;
            _dialougeService = dialougeService;
        }
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            _vectorSearchViewModel.ErrorMessage = null;
            _vectorSearchViewModel.IsLoading = true;
            try
            {
                throw new Exception();
                await _vectorSearchStore.Load(new SearchOptions()
                {
                    Text = _vectorSearchViewModel.SearchText,
                    IsVectorSearchEnabled = _vectorSearchViewModel.IsVectorSearchEnabled,
                    PageNumber = _vectorSearchViewModel.CurrentPage,
                    GloveType = _vectorSearchViewModel.GloveType
                });
            }
            catch (Exception ex)
            {
                _vectorSearchViewModel.ErrorMessage = ex.Message ?? "Some error occured";
                _dialougeService.ShowDialouge();
            }
            finally
            {
                _vectorSearchViewModel.IsLoading = false;
            }
        }
    }
}
