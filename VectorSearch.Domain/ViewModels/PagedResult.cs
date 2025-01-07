namespace VectorSearch.Domain.ViewModels
{
    public class PagedResult<T> where T : class
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int? TotalRecords { get; set; }
        public PagedResult()
        {
            Data = new List<T>();
        }
    }
}
