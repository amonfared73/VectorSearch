namespace VectorSearch.Domain.Configurations
{
    public class VectorSearchOptions
    {
        public IEnumerable<string> exclude { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
        public string SimilarityThreshold { get; set; }
        public string TakeNearesWords { get; set; }
    }
}