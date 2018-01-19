using System;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;
using PocoOrm.Core.Contract;
using PocoOrm.SqlServer;

namespace PocoOrm.Test
{
    [TestClass]
    public class SpecialContextTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        public void TestTableAttributeRequire()
        {
            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                new SpecialContext(new SqlConnection(), new Options());
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                throw;
            }
        }

        private class SpecialContext : SqlContext
        {
            public IRepository<ErrorType> Repository { get; set; }

            public SpecialContext(SqlConnection connection, Options options) : base(connection, options)
            {
            }

            public class ErrorType
            {
            }
        }
    }
}