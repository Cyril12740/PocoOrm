using System.Data.SqlClient;
using PocoOrm.Core;
using PocoOrm.Core.Contract;
using PocoOrm.SqlServer;

namespace PocoOrm.Console
{

    internal class Context : SqlContext
    {
        public Context(SqlConnection connection, Options options) : base(connection, options)
        {
        }

        public IRepository<TestTable> Test { get; set; }
    }
}