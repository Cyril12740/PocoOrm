using System;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class GroupedConditionBuilder : ISqlInverseBuilder
    {
        private readonly ISqlInverseBuilder _left;
        private readonly EnumCompare _comp;
        private readonly ISqlInverseBuilder _right;

        public GroupedConditionBuilder(ISqlInverseBuilder left, EnumCompare comp, ISqlInverseBuilder right)
        {
            if (!Enum.IsDefined(typeof(EnumCompare), comp))
            {
                throw new InvalidEnumArgumentException(nameof(comp), (int) comp, typeof(EnumCompare));
            }

            _left = left ?? throw new ArgumentNullException(nameof(left));
            _comp = comp;
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public string Build(ExpressionToSql parser, out DbParameter[] parameters)
        {
            string left = _left.Build(parser, out DbParameter[] leftParameters);
            string right = _right.Build(parser, out DbParameter[] rightParameters);
            parameters = leftParameters.Concat(rightParameters).ToArray();
            return $"( {left} {_comp.ToSql()} {right} )";
        }

        public ISqlInverseBuilder Inverse()
        {
            return new GroupedConditionBuilder(_left.Inverse(), _comp.Inverse(), _right.Inverse());
        }
    }
}