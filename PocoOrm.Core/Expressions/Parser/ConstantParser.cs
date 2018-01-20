using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class ConstantParser : Parser<ConstantExpression>
    {
        protected override ISqlBuilder Visit(ConstantExpression expression, ExpressionToSql parser)
        {
            return new SqlValueBuilder(expression.Value);
        }
    }
}