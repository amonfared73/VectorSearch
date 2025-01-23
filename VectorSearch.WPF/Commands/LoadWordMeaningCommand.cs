
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.ViewModels;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class LoadWordMeaningCommand : AsyncCommandBase
    {
        private readonly VectorSearchOptions _options;
        private readonly WordDetailViewModel _wordDetailViewModel;
        public LoadWordMeaningCommand(WordDetailViewModel wordDetailViewModel, VectorSearchOptions options)
        {
            _options = options;
            _wordDetailViewModel = wordDetailViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            throw new NotImplementedException();
            //try
            //{
            //    _wordDetailViewModel.IsLoading = true;
            //    var response = await _options.DictioanryUri
            //        .AppendPathSegment(_wordDetailViewModel.Word)
            //        .GetJsonAsync<DictionaryResultViewModel>();
            //    _wordDetailViewModel.IsLoading = false;
            //}
            //catch (Exception ex)
            //{
            //    _wordDetailViewModel.IsLoading = false;
            //}
            //finally
            //{
            //    _wordDetailViewModel.IsLoading = false;
            //}
        }
    }
}
