using System.Data.SqlClient;
using PocoOrm.SqlServer;

namespace PocoOrm.Console
{
    using Core;
    using Core.Contract;

    internal class Context : SqlContext
    {
        public Context(SqlConnection connection, ContextOptions options) : base(connection, options)
        {
        }

        public IRepository<TestTable> Test { get; set; }
    }
}