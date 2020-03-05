using System.ComponentModel;

namespace PocoOrm.Core.Contract.Expressions
{
    public enum EnumCompare
    {
        [Description("=")]           Equals,
        [Description("<>")]          NotEquals,
        [Description(">")]           GreaterThan,
        [Description(">=")]          GreaterThanOrEquals,
        [Description("<")]           LessThan,
        [Description("<=")]          LessThanOrEquals,
        [Description("AND")]         And,
        [Description("OR")]          Or,
        [Description("LIKE")]        Like,
        [Description("NOT LIKE")]    NotLike,
        [Description("IS NULL")]     IsNull,
        [Description("IS NOT NULL")] IsNotNull
    }
}