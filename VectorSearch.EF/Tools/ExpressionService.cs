using Azure;
using System.Linq.Expressions;
using VectorSearch.ApplicationService.Commands;
using VectorSearch.Domain.DTOs;
using VectorSearch.Domain.Enums;
using VectorSearch.Domain.Models;

namespace VectorSearch.EF.Tools
{
    public class ExpressionService : IExpressionService
    {
        public Vector CalculateVector(CalculateVectorRequest request)
        {
            ConstantExpression constFirst = Expression.Constant(request.FirstVector);
            ConstantExpression constSecond = Expression.Constant(request.SecondVector);
            ConstantExpression constThird = Expression.Constant(request.ThirdVector);

            BinaryExpression initialExpression = request.FirstOperation switch
            {
                WordCompareOperationType.Add => Expression.Add(constFirst, constSecond),
                WordCompareOperationType.Subtract => Expression.Subtract(constFirst, constSecond),
                _ => throw new InvalidOperationException("Unsupported operation"),
            };

            BinaryExpression finalExpression = request.SecondOperation switch
            {
                WordCompareOperationType.Add => Expression.Add(initialExpression, constThird),
                WordCompareOperationType.Subtract => Expression.Subtract(initialExpression, constThird),
                _ => throw new InvalidOperationException("Unsupported operation"),
            };

            var lambda = Expression.Lambda<Func<Vector>>(finalExpression).Compile();
            return lambda();
        }
    }
}
