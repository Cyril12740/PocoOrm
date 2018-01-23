using PocoOrm.Core;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Test.Stubs
{
    public class TestParameterBuilder : ParameterBuilder<TestParameter>
    {
        protected override TestParameter Build<TEntity>(string name, ColumnInformation<TEntity> column, TEntity value)
        {
            return new TestParameter(name, column.Information, column.Value(value));
        }

        protected override TestParameter Build(string name, ColumnAttribute column, object value)
        {
            return new TestParameter(name, column, value);
        }
    }
}