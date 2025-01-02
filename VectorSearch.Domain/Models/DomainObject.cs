namespace VectorSearch.Domain.Models
{
    public abstract class DomainObject
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
