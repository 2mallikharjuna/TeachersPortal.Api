
using System.Linq.Expressions;

namespace TeachPortal.Domain.Core.FetchStrategy
{
    public interface IIncludeDefinition
    {
        LambdaExpression IncludeExpression { get; }

        List<LambdaExpression> ThenIncludeExpressions { get; }

        Tuple<Expression, ParameterExpression> VisitThenInclude(LambdaExpression thenInclude);
    }
}
