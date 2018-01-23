using System.Data;
using System.Data.SqlClient;
using PocoOrm.Core;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.SqlServer
{
    internal class SqlParameterBuilder : ParameterBuilder<SqlParameter>
    {
        protected override SqlParameter Build<TEntity>(string name, ColumnInformation<TEntity> column, TEntity value)
        {
            return Build(name, column.Type, column.Size, column.Value(value));
        }

        protected override SqlParameter Build(string name, ColumnAttribute column, object value)
        {
            DbType columnType = column.Type;
            int? columnSize = column.Size;
            return Build(name, columnType, columnSize, value);
        }

        private SqlParameter Build(string name, DbType columnType, int? columnSize, object value)
        {
            return columnSize != null
                ? new SqlParameter
                {
                    ParameterName = name,
                    DbType = columnType,
                    Value = value,
                    Size = columnSize.Value
                }
                : new SqlParameter
                {
                    ParameterName = name,
                    DbType = columnType,
                    Value = value
                };
        }
    }
}