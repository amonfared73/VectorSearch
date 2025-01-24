namespace VectorSearch.Domain.Configurations
{
    public class VectorSearchOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string SimilarityThreshold { get; set; }
        public string TakeNearesWords { get; set; }
        public string DictioanryUri { get; set; }
    }
}