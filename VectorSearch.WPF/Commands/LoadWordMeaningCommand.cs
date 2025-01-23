using Flurl;
using Flurl.Http;
using VectorSearch.Domain.Configurations;
using VectorSearch.Domain.ViewModels;
using VectorSearch.WPF.Services;
using VectorSearch.WPF.ViewModels;

namespace VectorSearch.WPF.Commands
{
    public class LoadWordMeaningCommand : AsyncCommandBase
    {
        private readonly VectorSearchOptions _options;
        private readonly WordDetailViewModel _wordDetailViewModel;
        private readonly IDialougeService _dialougeService;
        public LoadWordMeaningCommand(WordDetailViewModel wordDetailViewModel, VectorSearchOptions options, IDialougeService dialougeService)
        {
            _options = options;
            _wordDetailViewModel = wordDetailViewModel;
            _dialougeService = dialougeService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _wordDetailViewModel.IsLoading = true;
                var response = await _options.DictioanryUri
                    .AppendPathSegment(_wordDetailViewModel.Word)
                    .GetJsonAsync<List<DictionaryResultViewModelItem>>();

                var phonetic = (from r in response select r.Phonetic).FirstOrDefault();


                var simplifiedResult = response
                    .SelectMany(result => result.Meanings, (result, meaning) => new
                    {
                        result.Phonetic,
                        meaning.PartOfSpeech,
                        Definitions = meaning.Definitions.Select(d => d.Definition)
                    })
                    .SelectMany(item => item.Definitions.Select(definition => new SimplifiedDictionaryResultItem()
                    {
                        PartOfSpeech = item.PartOfSpeech,
                        Definition = definition
                    }))
                    .ToList();
                _wordDetailViewModel.IsLoading = false;
            }
            catch (Exception ex)
            {
                _wordDetailViewModel.IsLoading = false;
                _dialougeService.ShowDialouge(options =>
                {
                    options.Message = ex.Message;
                    options.CloseText = "Close";
                    options.Title = "Error fetching meaning from dictionary";
                });
            }
            finally
            {
                _wordDetailViewModel.IsLoading = false;
            }
        }
    }
}
