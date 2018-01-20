using System;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions;
using PocoOrm.Test.Stubs;

namespace PocoOrm.Test
{
    [TestClass]
    public class ExpressionTest
    {
        private ExpressionToSql _builder;

        [TestInitialize]
        public void Initialize()
        {
            _builder = new ExpressionToSql(Options.Default.Use(new TestParameterBuilder()));
        }

        [TestMethod]
        public void TestEqual()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id == 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id = @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id != 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id <> @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestGreaterThan()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id > 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id > @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestGreaterThanOrEquals()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id >= 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id >= @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestLessThan()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id < 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id < @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestLessThanOrEquals()
        {
            Expression<Predicate<TestTable>> expression = table => table.Id <= 1;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id <= @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestIsNull()
        {
            Expression<Predicate<TestTable>> expression = table => table.Content == null;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Content IS NULL", sql);
            Assert.AreEqual(0, parameters.Length);
        }

        [TestMethod]
        public void TestIsNotNull()
        {
            Expression<Predicate<TestTable>> expression = table => table.Content != null;
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Content IS NOT NULL", sql);
            Assert.AreEqual(0, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseIsNotNull()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Content != null);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Content IS NULL", sql);
            Assert.AreEqual(0, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseIsNull()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Content == null);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Content IS NOT NULL", sql);
            Assert.AreEqual(0, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseEquals()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Content == "Bonjour");
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Content <> @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseNotEquals()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Content != "Bonjour");
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Content = @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseGreaterThan()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Id > 1);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id <= @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseGreaterThanOrEquals()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Id >= 1);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id < @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseLessThan()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Id < 1);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id >= @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }

        [TestMethod]
        public void TestWithInverseLessThanOrEquals()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Id <= 1);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("Id > @parameter1", sql);
            Assert.AreEqual(1, parameters.Length);
        }
    }
}