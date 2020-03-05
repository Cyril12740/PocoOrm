using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core
{
    public interface ISqlInverseBuilder : ISqlBuilder
    {
        ISqlInverseBuilder Inverse();
    }
}