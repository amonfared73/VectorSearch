namespace VectorSearch.Domain.Models
{
    public interface IWord
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Vector { get; set; }
        public int DictionaryTypeId { get; set; }
    }
}
