using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class GroupedConditionParser : IBinaryParser
    {
        public Type Left  => typeof(ISqlInverseBuilder);
        public Type Right => typeof(ISqlInverseBuilder);

        public ExpressionType[] Type => new[]
        {
            ExpressionType.And,
            ExpressionType.AndAlso,
            ExpressionType.Or,
            ExpressionType.OrElse
        };

        public ISqlInverseBuilder Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right)
        {
            return new GroupedConditionBuilder((ISqlInverseBuilder)left, comp, (ISqlInverseBuilder)right);
        }
    }
}