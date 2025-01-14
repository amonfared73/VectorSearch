using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.Tools;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class LoadCommand : AsyncCommandBase
    {
        private readonly PaginationType _paginationType;
        private readonly IDialougeService _dialougeService;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;

        public LoadCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore, IDialougeService dialougeService, PaginationType paginationType)
        {
            _paginationType = paginationType;
            _dialougeService = dialougeService;
            _vectorSearchStore = vectorSearchStore;
            _vectorSearchViewModel = vectorSearchViewModel;
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
                await _vectorSearchStore.Load(new SearchOptions()
                {
                    Text = _vectorSearchViewModel.SearchText,
                    IsVectorSearchEnabled = _vectorSearchViewModel.IsVectorSearchEnabled,
                    PageNumber = _vectorSearchViewModel.GetPageNumber(_paginationType),
                    GloveType = _vectorSearchViewModel.GloveType
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
                _vectorSearchViewModel.IsLoading = false;
            }
        }
    }
}