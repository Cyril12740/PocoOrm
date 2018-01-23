using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocoOrm.Core;
using PocoOrm.Core.Exceptions;
using PocoOrm.Test.Stubs;

namespace PocoOrm.Test
{
    [TestClass]
    public abstract class PrepareTest: IInterceptCommand
    {
        private const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        protected readonly SqlConnection Connection = new SqlConnection(ConnectionString);

        internal Context Context { get; private set; }

        [TestInitialize]
        public async Task Initialize()
        {
            InterceptCommand = null;
            await Connection.OpenAsync();
            await Execute("DELETE FROM Test");
            await Execute("DBCC CHECKIDENT ('Test', RESEED, 0)");
            await Execute("INSERT INTO Test VALUES ('Bonjour')");
            await Execute("INSERT INTO Test VALUES ('Salut')");
            await Execute("INSERT INTO Test VALUES ('Test')");
            Connection.Close();
            Context = new Context(Connection, Options.Default.Use(this));
        }
        private async Task Execute(string sql)
        {
            await new SqlCommand(sql, Connection).ExecuteNonQueryAsync();
        }

        [TestCleanup]
        public async Task Clean()
        {
            await Connection.OpenAsync();
            await Execute("DELETE FROM Test");
            Connection.Close();
        }

        public void Intercept(IDbCommand command)
        {
            InterceptCommand?.Invoke(command);
        }

        protected void Success()
        {
            throw new BreakExecutionException();
        }

        protected async Task Exectute(Func<Task> execute)
        {
            try
            {
                await execute();
            }
            catch (InterseptException e) when (e.InnerException is BreakExecutionException)
            {
            }
            catch (InterseptException e)
            {
                throw e.InnerException ?? e;
            }
        }

        protected event Action<IDbCommand> InterceptCommand;
    }
}