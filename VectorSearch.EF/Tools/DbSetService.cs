using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.Domain.Models;
using VectorSearch.EF.Contexts;

namespace VectorSearch.EF.Tools
{
    public class DbSetService : IDbSetService
    {
        public IQueryable<IWord> GetProperDbSet(SearchOptions searchOptions, VectorSearchDbContext context)
        {
            IQueryable<IWord> queryable;
            switch (searchOptions.GloveType)
            {
                case GloveType.glove_6B_50d:
                    queryable = context.Glove50Ds;
                    break;
                case GloveType.glove_6B_100d:
                    queryable = context.Glove100Ds;
                    break;
                case GloveType.glove_6B_200d:
                    queryable = context.Glove200Ds;
                    break;
                case GloveType.glove_6B_300d:
                    queryable = context.Glove300Ds;
                    break;
                case GloveType.WikipediaFarsi:
                    queryable = context.WikipediaFarsis;
                    break;
                default:
                    throw new ArgumentException("GloveType not assigned");
            }

            return queryable;
        }
    }
}
