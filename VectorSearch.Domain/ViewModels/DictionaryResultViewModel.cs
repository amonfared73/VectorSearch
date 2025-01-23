namespace VectorSearch.Domain.ViewModels
{
    public class DictionaryResultViewModel
    {
        public string word { get; set; }
        public string phonetic { get; set; }
        public List<Meaning> meanings { get; set; }
    }
    public class Meaning
    {
        public string partOfSpeech { get; set; }
        public List<Definition> definitions { get; set; }
    }
    public class Definition
    {
        public string definition { get; set; }
        public string example { get; set; }
    }
}
