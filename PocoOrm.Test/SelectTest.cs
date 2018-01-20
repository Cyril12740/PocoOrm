using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PocoOrm.Test
{
    [TestClass]
    public class SelectTest : PrepareTest
    {
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