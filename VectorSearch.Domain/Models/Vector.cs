namespace VectorSearch.Domain.Models
{
    public class Vector
    {
        public double[] Elements { get; }
        public Vector(double[] elements = null)
        {
            Elements = elements ?? new double[50];
        }

        public static Vector Empty => new Vector();

        public static Vector operator +(Vector left, Vector right)
        {
            var result = new double[50];
            for (int i = 0; i < 50; i++)
            {
                result[i] = left.Elements[i] + right.Elements[i];
            }
            return new Vector(result);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            var result = new double[50];
            for (int i = 0; i < 50; i++)
            {
                result[i] = left.Elements[i] - right.Elements[i];
            }
            return new Vector(result);
        }
    }
}
