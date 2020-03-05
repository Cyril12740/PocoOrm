using System.Data.Common;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core.Contract.Expressions
{
    public abstract class ParameterBuilder<TParameter> : IParameterBuilder
        where TParameter : DbParameter
    {
        protected abstract TParameter Build<TEntity>(string name, ColumnInformation<TEntity> column, TEntity value) where TEntity : class, new();
        protected abstract TParameter Build(string name, ColumnAttribute column, object value);

        DbParameter IParameterBuilder.Build(string name, ColumnAttribute column, object value)
        {
            return Build(name, column, value);
        }

        DbParameter IParameterBuilder.Build<TEntity>(string name, ColumnInformation<TEntity> column, TEntity value)
        {
            return Build(name, column, value);
        }
    }
}