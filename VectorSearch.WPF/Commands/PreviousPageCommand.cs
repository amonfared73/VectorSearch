using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class PreviousPageCommand : AsyncCommandBase
    {
        private readonly PaginationType _paginationType;
        private readonly IDialougeService _dialougeService;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;

        public PreviousPageCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore, IDialougeService dialougeService, PaginationType paginationType)
        {
            _dialougeService = dialougeService;
            _vectorSearchStore = vectorSearchStore;
            _vectorSearchViewModel = vectorSearchViewModel;
            _paginationType = paginationType;
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
                    PageNumber = getPageNumber(_paginationType),
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

        private int getPageNumber(PaginationType paginationType)
        {
            int pageNumber;
            switch (paginationType)
            {
                case PaginationType.CurrentPage:
                    pageNumber = _vectorSearchViewModel.CurrentPage;
                    break;
                case PaginationType.NextPage:
                    pageNumber = _vectorSearchViewModel.NextPage;
                    break;
                case PaginationType.PreviousPage:
                    pageNumber = _vectorSearchViewModel.PreviousPage;
                    break;
                default:
                    throw new ArgumentException("PaginationType not assigned");
            }
            return pageNumber;
        }
    }
}
