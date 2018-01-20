using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class BinaryParser : Parser<BinaryExpression>
    {
        protected override ISqlBuilder Visit(BinaryExpression expression, ExpressionToSql parser)
        {
            ISqlBuilder left = parser.Visit(expression.Left);
            ISqlBuilder right = parser.Visit(expression.Right);

            foreach (IBinaryParser binary in parser.Options.BinaryParser)
            {
                if (left.Is(binary.Left) && right.Is(binary.Right))
                {
                    return binary.Initialize(left, expression.NodeType.Convert(), right);
                }

                if (right.Is(binary.Left) && left.Is(binary.Right))
                {
                    return binary.Initialize(right, expression.NodeType.Convert(), left);
                }
            }

            throw new NotSupportedException();
        }
    }
}