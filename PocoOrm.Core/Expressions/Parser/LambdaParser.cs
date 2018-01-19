using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class LambdaParser : Parser<LambdaExpression>
    {
        protected override ISqlBuilder Visit(LambdaExpression expression, ExpressionToSql parser)
        {
            return parser.Visit(expression.Body);
        }
    }
}