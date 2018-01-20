using System;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IBinaryParser
    {
        Type Left { get; }

        Type Right { get; }

        ISqlInverseBuilder Initialize(ISqlBuilder left, EnumCompare comp, ISqlBuilder right);
    }
}