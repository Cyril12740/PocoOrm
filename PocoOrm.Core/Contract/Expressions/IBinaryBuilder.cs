using System;
using System.Data.Common;
using PocoOrm.Core.Expressions;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IBinaryBuilder
    {
        Type Left { get; }

        Type Right { get; }

        string Build(ExpressionToSql parser, ISqlBuilder left, object comp, ISqlBuilder right, out DbParameter[] parameter);
    }
}