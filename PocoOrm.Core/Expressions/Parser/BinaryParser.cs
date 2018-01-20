using System;
using System.Linq;
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
                if (!binary.Type.Contains(expression.NodeType))
                {
                    continue;
                }

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

    internal abstract class BinaryParser<TLeft, TRight> : IBinaryParser
        where TLeft : ISqlBuilder
        where TRight : ISqlBuilder
    {
        public Type Left => typeof(TLeft);

        public Type Right => typeof(TRight);

        public ExpressionType[] Type => new[]
        {
            ExpressionType.Equal,
            ExpressionType.NotEqual,
            ExpressionType.GreaterThan,
            ExpressionType.GreaterThanOrEqual,
            ExpressionType.LessThan,
            ExpressionType.LessThanOrEqual
        };

        ISqlInverseBuilder IBinaryParser.Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right)
        {
            return Initialize((TLeft)left, comp, (TRight)right);
        }

        protected abstract ISqlInverseBuilder Initialize(TLeft left, EnumCompare compare, TRight right);
    }
}