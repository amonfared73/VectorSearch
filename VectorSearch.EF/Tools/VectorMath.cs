namespace VectorSearch.EF.Tools
{
    public static class VectorMath
    {
        public static double ComputeCosineSimilarity(double[] vectorA, double[] vectorB)
        {
            double dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
            double magnitudeA = Math.Sqrt(vectorA.Sum(a => a * a));
            double magnitudeB = Math.Sqrt(vectorB.Sum(b => b * b));

            return dotProduct / (magnitudeA * magnitudeB);
        }
    }
}
