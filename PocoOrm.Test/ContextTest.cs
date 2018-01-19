using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;

namespace PocoOrm.Test
{
    [TestClass]
    public class ContextTest
    {
        private const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        private readonly SqlConnection _connection = new SqlConnection(ConnectionString);

        internal Context Context { get; private set; }

        [TestInitialize]
        public async Task Initialize()
        {
            await _connection.OpenAsync();

            await Execute("INSERT INTO Test VALUES ('Bonjour')");
            await Execute("INSERT INTO Test VALUES ('Salut')");
            await Execute("INSERT INTO Test VALUES ('Test')");

            _connection.Close();

            Context = new Context(_connection, new Options());
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

        [TestMethod]
        public void TestContextSetRepository()
        {
            Assert.IsNotNull(Context);
            Assert.IsNotNull(Context.Test);
        }

        [TestMethod]
        public async Task TestCanSelect()
        {
            IEnumerable<TestTable> result = await Context.Test.Select().ExecuteAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task TestCanSelectWithSimpleWhere()
        {
            IEnumerable<TestTable> result = await Context.Test.Select().Where(t => t.Content == "Salut").ExecuteAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}