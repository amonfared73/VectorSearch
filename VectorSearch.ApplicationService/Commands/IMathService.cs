﻿namespace VectorSearch.ApplicationService.Commands
{
    public interface IMathService
    {
        double ComputeCosineSimilarity(double[] left, double[] right);
        double[] ParseVector(string vectorString);
        string SerializeVector(double[] vector);
    }
}
