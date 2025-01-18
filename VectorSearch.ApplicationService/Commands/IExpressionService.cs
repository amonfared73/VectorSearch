using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Models;

namespace VectorSearch.ApplicationService.Commands
{
    public interface IExpressionService
    {
        Vector CalculateVector(CalculateVectorRequest request);
    }
}
