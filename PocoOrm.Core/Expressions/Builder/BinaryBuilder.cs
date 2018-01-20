using System;
using System.Data.Common;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Builder
{
    public abstract class BinaryBuilder<TLeft, TRight> : IBinaryBuilder
        where TLeft : ISqlBuilder
        where TRight : ISqlBuilder
    {
        public Type Left => typeof(TLeft);

        public Type Right => typeof(TRight);

        public string Build(ExpressionToSql parser,
                            ISqlBuilder left,
                            object comp,
                            ISqlBuilder right,
                            out DbParameter[] parameters)
        {
            return Build(parser, (TLeft) left, comp, (TRight) right, out parameters);
        }

        protected abstract string Build(ExpressionToSql parser,
                                        TLeft left,
                                        object comp,
                                        TRight right,
                                        out DbParameter[] parameters);
    }
}