namespace VectorSearch.Domain.ViewModels
{
    public class VectorResponseViewModel
    {
        public List<double> Vector { get; set; }
        public VectorResponseViewModel(List<double> vector)
        {
            Vector = vector ?? throw new ArgumentNullException(nameof(vector));
        }
        public double[] ToArray()
        {
            return Vector.ToArray();
        }
    }
}
