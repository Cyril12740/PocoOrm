using System.Data.Common;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class ColumnValueBuilder : ISqlInverseBuilder
    {
        private readonly EnumCompare      _compare;
        private readonly SqlColumnBuilder _left;
        private readonly SqlValueBuilder  _right;

        public ColumnValueBuilder(SqlColumnBuilder left, EnumCompare compare, SqlValueBuilder right)
        {
            _left = left;
            _compare = compare;
            _right = right;
        }

        public string Build(ExpressionToSql parser, out DbParameter[] parameters)
        {
            string sqlColumn = _left.Build(parser, out var _);
            string sqlParameter = _right.Build(parser, out parameters);

            parameters = new[]
            {
                parser.Options.ParameterBuilder.Build(sqlParameter, _left.Column, _right.Value)
            };

            return $"{sqlColumn} {_compare.ToSql()} {sqlParameter}";
        }

        public ISqlBuilder Inverse()
        {
            return new ColumnValueBuilder(_left, _compare.Inverse(), _right);
        }
    }
}