using System;
using System.Data.Common;
using System.Linq;
using PocoOrm.Core.Exceptions;
using PocoOrm.Core.Expressions.Parser;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class ColumnValueBuilder : BinaryBuilder<SqlColumnBuilder, SqlValueBuilder>
    {
        protected override string Build(ExpressionToSql parser, SqlColumnBuilder left, object comp, SqlValueBuilder right, out DbParameter[] parameters)
        {
            string sqlColumn = left.Build(parser, out var _);
            string sqlParameter = right.Build(parser, out parameters);

            parameters = new[]
            {
                parser.Options.ParameterBuilder.Build(sqlParameter, left.Column, right.Value)
            };

            return $"{sqlColumn} = {sqlParameter}";

        }
    }
}