using System.Data.Common;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IParameterBuilder
    {
        DbParameter Build(string name, ColumnAttribute column, object value);
    }

    public abstract class ParameterBuilder<TParameter> : IParameterBuilder
        where TParameter : DbParameter
    {
        DbParameter IParameterBuilder.Build(string name, ColumnAttribute column, object value)
        {
            return Build(name, column, value);
        }

        protected abstract TParameter Build(string name, ColumnAttribute column, object value);
    }
}