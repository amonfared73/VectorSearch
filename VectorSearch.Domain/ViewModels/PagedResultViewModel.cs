namespace VectorSearch.Domain.ViewModels
{
    public class PagedResultViewModel<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public long? TotalRecords { get; set; }
    }
}
