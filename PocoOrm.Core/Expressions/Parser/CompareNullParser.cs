using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class CompareNullParser : IBinaryParser
    {
        public Type Left  => typeof(SqlColumnBuilder);
        public Type Right => null;

        public ExpressionType[] Type => new[]
        {
            ExpressionType.Equal,
            ExpressionType.NotEqual
        };

        ISqlInverseBuilder IBinaryParser.Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right)
        {
            return new CompareNullBuilder(left, comp);
        }
    }
}