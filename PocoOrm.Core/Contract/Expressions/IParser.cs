using System;
using System.Linq.Expressions;
using PocoOrm.Core.Expressions;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IParser
    {
        Type Type { get; }

        ISqlBuilder Visit(Expression expression, ExpressionToSql builder);
    }
}