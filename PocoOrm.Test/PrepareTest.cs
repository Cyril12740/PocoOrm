using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;
using PocoOrm.Test.Stubs;

namespace PocoOrm.Test
{
    [TestClass]
    public abstract class PrepareTest
    {
        private const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        protected readonly SqlConnection _connection = new SqlConnection(ConnectionString);

        internal Context Context { get; private set; }

        [TestInitialize]
        public async Task Initialize()
        {
            await _connection.OpenAsync();
            await Execute("DBCC CHECKIDENT ('Test', RESEED, 0)");
            await Execute("INSERT INTO Test VALUES ('Bonjour')");
            await Execute("INSERT INTO Test VALUES ('Salut')");
            await Execute("INSERT INTO Test VALUES ('Test')");
            _connection.Close();
            Context = new Context(_connection, Options.Default);
        }
        private async Task Execute(string sql)
        {
            await new SqlCommand(sql, _connection).ExecuteNonQueryAsync();
        }

        [TestCleanup]
        public async Task Clean()
        {
            await _connection.OpenAsync();
            await Execute("DELETE FROM Test");
            _connection.Close();
        }
    }
}