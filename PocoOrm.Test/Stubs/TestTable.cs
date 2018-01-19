using System.Data;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Test
{
    [Table("Test")]
    internal class TestTable
    {
        [Column("Content", DbType.String)]
        public string Content { get; set; }

        [Column("Id", DbType.Int32)]
        public int Id { get; set; }
    }
}