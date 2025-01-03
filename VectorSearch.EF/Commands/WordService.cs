using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.Models;
using VectorSearch.EF.Contexts;

namespace VectorSearch.EF.Commands
{
    public class WordService : BaseService<Word>, IWordService
    {
        private readonly VectorSearchDbContextFactory _contextFactory;
        public WordService(VectorSearchDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
