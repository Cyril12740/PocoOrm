using System;
using System.Linq.Expressions;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IBinaryParser
    {
        Type Left { get; }

        Type Right { get; }

        ExpressionType[] Type { get; }

        ISqlInverseBuilder Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right);
    }
}