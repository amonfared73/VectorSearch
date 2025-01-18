using VectorSearch.Domain.Enums;

namespace VectorSearch.Domain.DTOs
{
    public class CompareWordsRequestViewModel
    {
        public string FirstWord { get; set; }
        public string SecondWord { get; set; }
        public string ThirdWord { get; set; }
        public WordCompareOperationType FirstOperation { get; set; }
        public WordCompareOperationType SecondOperation { get; set; }
    }
}
