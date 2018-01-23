using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Test.Stubs;

namespace PocoOrm.Test
{
    [TestClass]
    public class DeleteTest : PrepareTest
    {

        [TestMethod]
        public async Task TestDelete()
        {
            string content = Guid.NewGuid().ToString();
            TestTable[] insertedEntities = (await Context.Test.Select().ExecuteAsync()).ToArray();
            Assert.AreEqual(3, insertedEntities.Length);
            var deleted = await Context.Test.Delete(insertedEntities).ExecuteAsync();
            Assert.AreEqual(insertedEntities.Length, deleted);
        }
    }
}