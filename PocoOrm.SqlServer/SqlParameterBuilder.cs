using System.Data.SqlClient;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.SqlServer
{
    internal class SqlParameterBuilder : ParameterBuilder<SqlParameter>
    {
        protected override SqlParameter Build(string name, ColumnAttribute column, object value)
        {
            return column.Size != null
                       ? new SqlParameter
                       {
                           ParameterName = name,
                           DbType = column.Type,
                           Value = value,
                           Size = column.Size.Value
                       }
                       : new SqlParameter
                       {
                           ParameterName = name,
                           DbType = column.Type,
                           Value = value
                       };
        }
    }
}