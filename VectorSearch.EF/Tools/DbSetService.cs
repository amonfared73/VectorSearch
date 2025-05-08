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
            switch (searchOptions.SourceType)
            {
                case SourceType.glove_6B_50d:
                    queryable = context.Glove50Ds;
                    break;
                case SourceType.glove_6B_100d:
                    queryable = context.Glove100Ds;
                    break;
                case SourceType.glove_6B_200d:
                    queryable = context.Glove200Ds;
                    break;
                case SourceType.glove_6B_300d:
                    queryable = context.Glove300Ds;
                    break;
                case SourceType.WikipediaFarsi:
                    queryable = context.WikipediaFarsis;
                    break;
                case SourceType.digikala_goods:
                    queryable = context.DigikalaGoods;
                    break;
                case SourceType.faranShimi:
                    queryable = context.FaranShimiGoods;
                    break;
                case SourceType.padidehShimiGharb:
                    queryable = context.padideShimiGharbGoods;
                    break;
                default:
                    throw new ArgumentException("SourceType not assigned");
            }

            return queryable;
        }
    }
}
