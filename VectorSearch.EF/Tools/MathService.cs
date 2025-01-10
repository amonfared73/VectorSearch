using VectorSearch.ApplicationService.Commands;

namespace VectorSearch.EF.Tools
{
    public class MathService : IMathService
    {
        public double ComputeCosineSimilarity(double[] left, double[] right)
        {
            double dotProduct = left.Zip(right, (a, b) => a * b).Sum();
            double magnitudeA = Math.Sqrt(left.Sum(a => a * a));
            double magnitudeB = Math.Sqrt(right.Sum(b => b * b));

            return dotProduct / (magnitudeA * magnitudeB);
        }

        public double[] ParseVector(string vectorString)
        {
            return vectorString.Split(' ').Select(double.Parse).ToArray();
        }

        public string SerializeVector(double[] vector)
        {
            return string.Join(" ", vector);
        }
    }
}
