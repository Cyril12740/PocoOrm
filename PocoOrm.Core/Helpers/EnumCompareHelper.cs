using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.Core.Helpers
{
    public static class EnumCompareHelper
    {
        public static EnumCompare Convert(this ExpressionType type)
        {
            if (!Enum.IsDefined(typeof(ExpressionType), type))
            {
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(ExpressionType));
            }

            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return EnumCompare.And;
                case ExpressionType.Equal:
                    return EnumCompare.Equals;
                case ExpressionType.GreaterThan:
                    return EnumCompare.GreaterThan;
                case ExpressionType.GreaterThanOrEqual:
                    return EnumCompare.GreaterThanOrEquals;
                case ExpressionType.LessThan:
                    return EnumCompare.LessThan;
                case ExpressionType.LessThanOrEqual:
                    return EnumCompare.LessThanOrEquals;
                case ExpressionType.Not:
                    throw new NotImplementedException("Expression must be inverted");
                case ExpressionType.NotEqual:
                    return EnumCompare.NotEquals;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return EnumCompare.Or;
                case ExpressionType.Assign:
                    throw new NotImplementedException("Expression must be inverted");
            }

            throw new NotSupportedException($"{type.GetType().Name}.{type.ToString()}");
        }

        public static string ToSql(this EnumCompare compare)
        {
            FieldInfo fi = compare.GetType().GetField(compare.ToString());

            DescriptionAttribute attributes = fi.GetCustomAttribute<DescriptionAttribute>();

            return attributes?.Description ?? throw new Exception("no description"); //todo : default type
        }

        public static EnumCompare Inverse(this EnumCompare compare)
        {
            switch (compare)
            {
                case EnumCompare.Equals:
                    return EnumCompare.NotEquals;
                case EnumCompare.NotEquals:
                    return EnumCompare.Equals;
                case EnumCompare.GreaterThan:
                    return EnumCompare.LessThanOrEquals;
                case EnumCompare.GreaterThanOrEquals:
                    return EnumCompare.LessThan;
                case EnumCompare.LessThan:
                    return EnumCompare.GreaterThanOrEquals;
                case EnumCompare.LessThanOrEquals:
                    return EnumCompare.GreaterThan;
                case EnumCompare.And:
                    return EnumCompare.Or;
                case EnumCompare.Or:
                    return EnumCompare.And;
                case EnumCompare.Like:
                    return EnumCompare.NotLike;
                case EnumCompare.NotLike:
                    return EnumCompare.Like;
                case EnumCompare.IsNull:
                    return EnumCompare.IsNotNull;
                case EnumCompare.IsNotNull:
                    return EnumCompare.IsNull;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compare), compare, null);
            }
        }
    }
}