using System.Data.SqlClient;
using PocoOrm.Core;
using PocoOrm.Core.Contract;
using PocoOrm.SqlServer;

namespace PocoOrm.Test.Stubs
{
    internal class Context : SqlContext
    {
        public IRepository<TestTable> Test { get; set; }

        public Context(SqlConnection connection, Options options) : base(connection, options)
        {
        }
    }
}