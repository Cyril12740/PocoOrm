using System.Data.Common;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class SqlValueBuilder : ISqlBuilder
    {
        public object Value { get; }

        public SqlValueBuilder(object expressionValue)
        {
            Value = expressionValue;
        }

        public string Build(ExpressionToSql parser, out DbParameter[] parameters)
        {
            string parameterName = $"@parameter{parser.Parameter}";
            parameters = new DbParameter[0];
            return parameterName;
        }
    }
}