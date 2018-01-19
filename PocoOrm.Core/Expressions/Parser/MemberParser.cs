using System;
using System.Linq.Expressions;
using System.Reflection;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;

namespace PocoOrm.Core.Expressions.Parser
{
    internal class MemberParser : Parser<MemberExpression>
    {
        protected override ISqlBuilder Visit(MemberExpression expression, ExpressionToSql parser)
        {
            if (expression.Expression is ParameterExpression)
            {
                ColumnAttribute attribute = expression.Member.GetCustomAttribute<ColumnAttribute>();
                if (attribute == null)
                {
                    throw new ArgumentException($"{expression.Member.Name} has not a column attribute");
                }
                return new SqlColumnBuilder(attribute);
            }

            throw new NotSupportedException("seams to be a FK");
        }
    }
}