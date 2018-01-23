using System.Data.Common;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IParameterBuilder
    {
        DbParameter Build<TEntity>(string name, ColumnInformation<TEntity> column, TEntity value) where TEntity : class, new();

        DbParameter Build(string name, ColumnAttribute column, object value);
    }
}