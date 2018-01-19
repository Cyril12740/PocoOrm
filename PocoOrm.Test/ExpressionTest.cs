using System;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions;

namespace PocoOrm.Test
{
    [TestClass]
    public class ExpressionTest
    {
        private ExpressionToSql _builder;

        [TestInitialize]
        public void Initialize()
        {
            _builder = new ExpressionToSql(new Options().Use(new TestParameterBuilder()));
        }

        [TestMethod]
        public void TestContextSetRepository()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id == 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);

            Assert.AreEqual("Id = @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

    }
}