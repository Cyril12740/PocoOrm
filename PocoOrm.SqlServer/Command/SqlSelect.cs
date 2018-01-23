using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PocoOrm.Core.Command;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions;
using PocoOrm.SqlServer.Helpers;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlSelect<TEntity> : ReaderExecute<TEntity>, ISelect<TEntity> where TEntity : class, new()
    {
        protected new SqlRepository<TEntity> Repository => base.Repository as SqlRepository<TEntity>;
        private Expression<Predicate<TEntity>> _expression;
        private int _counter;

        public SqlSelect(SqlRepository<TEntity> repository): base(repository)
        {
        }

        public override async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            SqlCommand cmd = Repository.Context.Connection.CreateCommand();

            if (_expression != null)
            {
                ExpressionToSql parser = new ExpressionToSql(this, Repository.Context.Options);

                ISqlBuilder builder = parser.Visit(_expression);

                string whereClause = builder.Build(parser, out DbParameter[] parameters);

                cmd.CommandText = $"SELECT * FROM  {Repository.Information.Name} WHERE {whereClause}";

                foreach (DbParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            else
            {
                cmd.CommandText = $"SELECT * FROM  {Repository.Information.Name}";
            }
            return await cmd.Connection.OpenDatabase(async () => await ExecuteReaderAsync(cmd));
        }

        public ISelect<TEntity> Where(Expression<Predicate<TEntity>> expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
            return this;
        }

        public ISelect<TEntity> OderBy(Expression<Func<TEntity, object>> expression, bool desc = false)
        {
            throw new NotImplementedException();
        }
        public string ParameterName => $"@parameter{++_counter}";
    }
}