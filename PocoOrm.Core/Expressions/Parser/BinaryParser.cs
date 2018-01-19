using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class BinaryParser : Parser<BinaryExpression>
    {
        private readonly Options _options;

        public BinaryParser(Options options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected override ISqlBuilder Visit(BinaryExpression expression, ExpressionToSql parser)
        {
            ISqlBuilder left = parser.Visit(expression.Left);
            ISqlBuilder right = parser.Visit(expression.Right);
            return new SqlBinaryBuilder(_options, left, expression.NodeType, right);
        }
    }
}