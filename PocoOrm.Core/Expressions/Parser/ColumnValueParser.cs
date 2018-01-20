using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class ColumnValueParser : BinaryParser<SqlColumnBuilder, SqlValueBuilder>
    {
        protected override ISqlInverseBuilder Initialize(SqlColumnBuilder left,
                                                         EnumCompare compare,
                                                         SqlValueBuilder right)
        {
            return new ColumnValueBuilder(left, compare, right);
        }
    }
}