using System.Data.Common;
using PocoOrm.Core.Expressions;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface ISqlBuilder
    {
        string Build(ExpressionToSql parser, out DbParameter[] parameters);
    }
}