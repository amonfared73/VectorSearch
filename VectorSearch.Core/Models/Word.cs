namespace VectorSearch.Core.Models
{
    public class Word : DomainObject
    {
        public string Text { get; set; } = string.Empty;
        public byte[]? Embeddings { get; set; }
    }
}
