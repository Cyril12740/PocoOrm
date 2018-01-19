using System;
using System.Data.Common;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class SqlColumnBuilder : ISqlBuilder
    {
        public ColumnAttribute Column { get; }

        public SqlColumnBuilder(ColumnAttribute attribute)
        {
            Column = attribute;
        }

        public string Build(ExpressionToSql parser, out DbParameter[] parameter)
        {
            parameter = new DbParameter[0];
            return Column.Name;
        }
    }
}