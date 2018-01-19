using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Test
{
    public class TestParameterBuilder : ParameterBuilder<TestParameter>
    {
        protected override TestParameter Build(string name, ColumnAttribute column, object value)
        {
            return new TestParameter(name, column, value);
        }
    }
}