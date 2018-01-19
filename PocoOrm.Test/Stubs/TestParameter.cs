using System.Data;
using System.Data.Common;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Test
{
    public sealed class TestParameter : DbParameter
    {
        public TestParameter(string name, ColumnAttribute column, object value)
        {
            ParameterName = name;
            DbType = column.Type;
            Size = column.Size ?? 0;
            Value = value;
        }

        public override void ResetDbType()
        {
        }

        public override DbType DbType { get; set; }

        public override ParameterDirection Direction { get; set; }

        public override bool IsNullable { get; set; }

        public override string ParameterName { get; set; }

        public override string SourceColumn { get; set; }

        public override object Value { get; set; }

        public override bool SourceColumnNullMapping { get; set; }

        public override int Size { get; set; }
    }
}