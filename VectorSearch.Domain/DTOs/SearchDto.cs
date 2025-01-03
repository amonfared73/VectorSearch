namespace VectorSearch.Domain.DTOs
{
    public class SearchDto
    {
        public string? Text { get; set; } = string.Empty;
        public bool IsVectorSearchEnabled { get; set; } = false;
    }
}
