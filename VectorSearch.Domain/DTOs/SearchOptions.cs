using VectorSearch.Domain.Enums;

namespace VectorSearch.Domain.DTOs
{
    public class SearchOptions
    {
        public string? Text { get; set; } = string.Empty;
        public bool IsVectorSearchEnabled { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public SourceType SourceType { get; set; } = SourceType.glove_6B_50d;
    }
}
