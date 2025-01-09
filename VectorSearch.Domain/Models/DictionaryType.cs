namespace VectorSearch.Domain.Models
{
    public class DictionaryType : DomainObject
    {
        public string Name { get; set; }
        public ICollection<Word>? Words { get; set; }
    }
}
