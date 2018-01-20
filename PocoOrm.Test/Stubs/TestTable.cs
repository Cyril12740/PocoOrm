using System.Data;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Test.Stubs
{
    [Table("Test")]
    internal class TestTable
    {
        [Column("Content", DbType.String)]
        public string Content { get; set; }

        [Column("Id", DbType.Int32), PrimaryKey(true)]
        public int Id { get; set; }
    }
}