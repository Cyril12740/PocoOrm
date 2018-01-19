using System;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Parser
{
    public abstract class Parser<TExpression> : IParser
        where TExpression : Expression
    {
        public Type Type => typeof(TExpression);

        public ISqlBuilder Visit(Expression expression, ExpressionToSql builder)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (expression is TExpression casted)
            {
                return Visit(casted, builder);
            }
            throw new ArgumentException($"not supported {expression.GetType().Name}", nameof(expression));
        }

        protected abstract ISqlBuilder Visit(TExpression expression, ExpressionToSql parser);
    }
}