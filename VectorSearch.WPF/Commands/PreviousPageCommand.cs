﻿using VectorSearch.Domain.DTOs;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.Stores;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class PreviousPageCommand : AsyncCommandBase
    {
        private readonly IDialougeService _dialougeService;
        private readonly VectorSearchStore _vectorSearchStore;
        private readonly VectorSearchViewModel _vectorSearchViewModel;

        public PreviousPageCommand(VectorSearchViewModel vectorSearchViewModel, VectorSearchStore vectorSearchStore, IDialougeService dialougeService)
        {
            _dialougeService = dialougeService;
            _vectorSearchStore = vectorSearchStore;
            _vectorSearchViewModel = vectorSearchViewModel;
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
