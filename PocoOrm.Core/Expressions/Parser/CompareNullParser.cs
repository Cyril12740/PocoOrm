using System;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class CompareNullParser : IBinaryParser
    {
        public Type Left  => typeof(SqlColumnBuilder);
        public Type Right => null;

        ISqlInverseBuilder IBinaryParser.Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right)
        {
            return new CompareNullBuilder(left, comp);
        }
    }
}