namespace VectorSearch.EF.Tools
{
    public static class VectorParser
    {
        public static double[] ParseVector(this string vectorString)
        {
            return vectorString.Split(' ')
                               .Select(double.Parse)
                               .ToArray();
        }

        public static string SerializeVector(this double[] vector)
        {
            return string.Join(" ", vector);
        }
    }
}
