using System;
using System.Data.Common;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class SqlBinaryBuilder : ISqlBuilder
    {
        private readonly ExpressionType _compare;

        private readonly ISqlBuilder _left;

        private readonly Options _options;

        private readonly ISqlBuilder _right;

        public SqlBinaryBuilder(Options options, ISqlBuilder left, ExpressionType compare, ISqlBuilder right)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _left = left;
            _compare = compare;
            _right = right;
        }

        public string Build(ExpressionToSql parser, out DbParameter[] parameters)
        {
            foreach (IBinaryBuilder binaryBuilder in _options.BinaryParser)
            {
                if (_left.Is(binaryBuilder.Left) && _right.Is(binaryBuilder.Right))
                {
                    return binaryBuilder.Build(parser, _left, _compare, _right, out parameters);
                }

                if (_right.Is(binaryBuilder.Left) && _left.Is(binaryBuilder.Right))
                {
                    return binaryBuilder.Build(parser, _right, _compare, _left, out parameters);
                }
            }

            throw new NotSupportedException();
        }
    }
}