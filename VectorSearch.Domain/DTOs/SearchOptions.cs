namespace VectorSearch.Domain.DTOs
{
    public class SearchOptions
    {
        public string? Text { get; set; } = string.Empty;
        public bool IsVectorSearchEnabled { get; set; } = false;
    }
}
