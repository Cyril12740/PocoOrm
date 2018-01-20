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

        [TestMethod]
        public void TestWithAnd()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => table.Id == 1 && table.Content == "Bonjour";
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id = @parameter1 AND Content = @parameter2 )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }
        [TestMethod]
        public void TestWithAnd2()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => (table.Id == 1) & (table.Content == "Bonjour");
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id = @parameter1 AND Content = @parameter2 )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }
        [TestMethod]
        public void TestWithOr()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => table.Id == 1 || table.Content == "Bonjour";
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id = @parameter1 OR Content = @parameter2 )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }
        [TestMethod]
        public void TestWithOr2()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => (table.Id == 1) | (table.Content == "Bonjour");
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id = @parameter1 OR Content = @parameter2 )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }


        [TestMethod]
        public void TestCompexeCondition()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => table.Id == 1 && table.Content != null && table.Content == "Bonjour";
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( ( Id = @parameter1 AND Content IS NOT NULL ) AND Content = @parameter2 )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }

        [TestMethod]
        public void TestCompexeCondition2()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => table.Id == 1 || table.Content != null && table.Content == "Bonjour";
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id = @parameter1 OR ( Content IS NOT NULL AND Content = @parameter2 ) )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }

        [TestMethod]
        public void TestCompexeCondition3()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => table.Id == 1 && table.Content == null || table.Content == "Bonjour";
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( ( Id = @parameter1 AND Content IS NULL ) OR Content = @parameter2 )", sql);
            Assert.AreEqual(2, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(1, parameters[0].Value);
            Assert.AreEqual("@parameter2", parameters[1].ParameterName);
            Assert.AreEqual("Bonjour", parameters[1].Value);
        }

        [TestMethod]
        public void TestInverseAnd()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Id <= 2 && table.Content == null);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id > @parameter1 OR Content IS NOT NULL )", sql);
            Assert.AreEqual(1, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(2, parameters[0].Value);
        }

        [TestMethod]
        public void TestInverseOr()
        {
            // ReSharper disable once NegativeEqualityExpression
            Expression<Predicate<TestTable>> expression = table => !(table.Id <= 2 || table.Content == null);
            ISqlBuilder builder = _builder.Visit(expression);
            string sql = builder.Build(_builder, out DbParameter[] parameters);
            Assert.AreEqual("( Id > @parameter1 AND Content IS NOT NULL )", sql);
            Assert.AreEqual(1, parameters.Length);
            Assert.AreEqual("@parameter1", parameters[0].ParameterName);
            Assert.AreEqual(2, parameters[0].Value);
        }
    }
}