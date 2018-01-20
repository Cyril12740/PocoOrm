using System;
using System.Data.Common;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions.Builder
{
    internal class CompareNullBuilder : ISqlInverseBuilder
    {
        private readonly EnumCompare _comp;
        private readonly ISqlBuilder _left;

        public CompareNullBuilder(ISqlBuilder left, EnumCompare comp)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));

            switch (comp)
            {
                case EnumCompare.Equals:
                    _comp = EnumCompare.IsNull;
                    break;
                case EnumCompare.NotEquals:
                    _comp = EnumCompare.IsNotNull;
                    break;
                case EnumCompare.IsNotNull:
                case EnumCompare.IsNull:
                    _comp = comp;
                    break;
                default:
                    throw new ArgumentException($"Impossible comparaison with null and {comp.ToString()}");
            }
        }

        public string Build(ExpressionToSql parser, out DbParameter[] parameters)
        {
            string sqlColumn = _left.Build(parser, out var _);
            parameters = new DbParameter[0];

            return $"{sqlColumn} {_comp.ToSql()}";
        }

        public ISqlBuilder Inverse()
        {
            return new CompareNullBuilder(_left, _comp.Inverse());
        }
    }
}