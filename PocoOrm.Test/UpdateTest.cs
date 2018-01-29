using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;
using PocoOrm.Core.Helpers;
using PocoOrm.SqlServer;
using PocoOrm.Test.Stubs;

namespace PocoOrm.Test
{
    [TestClass]
    public class UpdateTest : PrepareTest
    {
        [TestMethod]
        public void Test1()
        {
            //todo so stupid
            var builder = new UpdateBuilder<TestTable>(new TableInformation<TestTable>(), new SqlParameterBuilder(), new ParameterCounter());
            var result = builder.Prepare(new TestTable()
            {
                Content = "dsfgdf",
                Id = 1
            });
            Assert.AreEqual(2, result.Parameters.Count());
            Assert.AreEqual("Content = @parameter2", result.Sql);
            Assert.AreEqual("Id = @parameter1", result.Where);
        }
    }
}