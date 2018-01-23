using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions;
using PocoOrm.Core.Helpers;
using PocoOrm.SqlServer.Helpers;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlSelect<TEntity> : ISelect<TEntity> where TEntity : class, new()
    {
        private readonly SqlRepository<TEntity> _repository;

        private Expression<Predicate<TEntity>> _expression;

        public SqlSelect(SqlRepository<TEntity> sqlRepository)
        {
            _repository = sqlRepository ?? throw new ArgumentNullException(nameof(sqlRepository));
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            SqlCommand cmd = _repository.Context.Connection.CreateCommand();

            if (_expression != null)
            {
                ExpressionToSql parser = new ExpressionToSql(_repository.Context.Options);

                ISqlBuilder builder = parser.Visit(_expression);

                string whereClause = builder.Build(parser, out DbParameter[] parameters);

                cmd.CommandText = $"SELECT * FROM  {_repository.Information.Name} WHERE {whereClause}";

                foreach (DbParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            else
            {
                cmd.CommandText = $"SELECT * FROM  {_repository.Information.Name}";
            }

            return await cmd.Connection.OpenDatabase(async () => await cmd.ExecuteReaderAsync(_repository.Mapper));
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