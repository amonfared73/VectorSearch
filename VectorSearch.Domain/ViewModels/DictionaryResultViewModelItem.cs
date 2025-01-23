namespace VectorSearch.Domain.ViewModels
{
    public class DictionaryResultViewModel
    {
        public List<DictionaryResultViewModelItem> Result { get; set; }
    }

    public class SimplifiedDictionaryResultItem
    {
        public string PartOfSpeech { get; set; }
        public string Definition { get; set; }
    }
    public class DictionaryResultViewModelItem
    {
        public string Word { get; set; }
        public string Phonetic { get; set; }
        public List<Phonetic> Phonetics { get; set; }
        public List<Meaning> Meanings { get; set; }
        public License License { get; set; }
        public List<string> SourceUrls { get; set; }
    }
    public class License
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Phonetic
    {
        public string Text { get; set; }
        public string Audio { get; set; }
        public string SourceUrl { get; set; }
        public License License { get; set; }
    }

    public class DefinitionItem
    {
        public string Definition { get; set; } // Changed the name to avoid conflict with the class name
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
        public string Example { get; set; }
    }

    public class Meaning
    {
        public string PartOfSpeech { get; set; }
        public List<DefinitionItem> Definitions { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
    }
}
