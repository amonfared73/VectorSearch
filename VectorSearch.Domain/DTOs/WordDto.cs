namespace VectorSearch.Domain.DTOs
{
    public class WordDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public double Similarity { get; set; }
        public string Vector { get; set; } = string.Empty;
        public int DictionaryTypeId { get; set; }
    }
}
