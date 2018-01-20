using System;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Builder
{
    public abstract class BinaryBuilder<TLeft, TRight> : IBinaryParser
        where TLeft : ISqlBuilder
        where TRight : ISqlBuilder
    {
        public Type Left => typeof(TLeft);

        public Type Right => typeof(TRight);

        ISqlInverseBuilder IBinaryParser.Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right)
        {
            return Initialize((TLeft) left, comp, (TRight) right);
        }

        protected abstract ISqlInverseBuilder Initialize(TLeft left, EnumCompare compare, TRight right);
    }
}