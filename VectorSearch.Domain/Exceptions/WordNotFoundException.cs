namespace VectorSearch.Domain.Exceptions
{
    public class WordNotFoundException : Exception
    {
        public WordNotFoundException(string? message) : base(message)
        {

        }
    }
}
