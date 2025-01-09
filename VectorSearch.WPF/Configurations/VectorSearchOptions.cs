namespace VectorSearch.WPF.Configurations
{
    public class VectorSearchOptions
    {
        public IEnumerable<string> exclude { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
        public double SimilarityThreshold { get; set; }
    }
}