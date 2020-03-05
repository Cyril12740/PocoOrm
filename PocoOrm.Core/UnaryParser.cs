using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions;
using PocoOrm.Core.Expressions.Parser;

namespace PocoOrm.Core
{
    internal class UnaryParser : Parser<UnaryExpression>
    {
        protected override ISqlBuilder Visit(UnaryExpression expression, ExpressionToSql parser)
        {
            ISqlBuilder parent = parser.Visit(expression.Operand);

            if (parent is ISqlInverseBuilder builder)
            {
                return builder.Inverse();
            }

            throw new NotSupportedException();
        }
    }
}