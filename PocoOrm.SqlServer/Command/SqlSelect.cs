using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PocoOrm.Core.Command;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlSelect<TEntity> : ReaderExecute<TEntity>, ISelect<TEntity> where TEntity : class, new()
    {
        private Expression<Predicate<TEntity>> _expression;

        public SqlSelect(IRepository<TEntity> repository): base(repository)
        {
        }

        public override async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            IDbCommand cmd = Repository.Context.Connection.CreateCommand();

            if (_expression != null)
            {
                ExpressionToSql parser = new ExpressionToSql(Repository.Context.Options);

                ISqlBuilder builder = parser.Visit(_expression);

                string whereClause = builder.Build(parser, out DbParameter[] parameters);

                cmd.CommandText = $"SELECT * FROM  {Repository.TableName} WHERE {whereClause}";

                foreach (DbParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            else
            {
                cmd.CommandText = $"SELECT * FROM  {Repository.TableName}";
            }

            return await ExecuteReaderAsync(cmd);
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
    }
}