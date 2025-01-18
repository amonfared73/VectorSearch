using VectorSearch.Domain.Enums;
using VectorSearch.Domain.Models;

namespace VectorSearch.Domain.DTOs
{
    public class CalculateVectorRequest
    {
        public Vector FirstVector { get; set; }
        public Vector SecondVector { get; set; }
        public Vector ThirdVector { get; set; }
        public WordCompareOperationType FirstOperation { get; set; }
        public WordCompareOperationType SecondOperation { get; set; }

    }
}
