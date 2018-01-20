using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core.Helpers;
using PocoOrm.Test.Stubs;

namespace PocoOrm.Test
{
    [TestClass]
    public class InsertTest : PrepareTest
    {
        [TestMethod]
        public async Task TestInsert()
        {
            string content = Guid.NewGuid().ToString();
            TestTable[] insertedEntities = (await Context.Test.Insert(new TestTable
                                                         {
                                                             Content = content
                                                         })
                                                         .ExecuteAsync()).ToArray();
            SqlCommand cmd = new SqlCommand($"select count(*) from Test where Content = '{content}'", _connection);
            await _connection.OpenAsync();
            int result = (int) await cmd.ExecuteScalarAsync();
            _connection.Close();
            Assert.AreEqual(1, result, "nombre de ligne inséré");
            Assert.AreEqual(1, insertedEntities.Length);
            Assert.AreEqual(content, insertedEntities[0].Content);
        }
    }
}