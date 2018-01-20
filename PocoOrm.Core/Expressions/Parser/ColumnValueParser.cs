using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class ColumnValueParser : BinaryBuilder<SqlColumnBuilder, SqlValueBuilder>
    {
        protected override ISqlInverseBuilder Initialize(SqlColumnBuilder left,
                                                         EnumCompare compare,
                                                         SqlValueBuilder right)
        {
            return new ColumnValueBuilder(left, compare, right);
        }
    }
}