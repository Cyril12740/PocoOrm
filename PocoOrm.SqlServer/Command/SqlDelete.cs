using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PocoOrm.Core.Contract.Command;
using PocoOrm.SqlServer.Helpers;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlDelete<TEntity> : IExecutable<int> where TEntity : class, new()
    {
        private readonly SqlRepository<TEntity> _repository;
        private readonly TEntity[] _entities;

        public SqlDelete(SqlRepository<TEntity> repository, TEntity[] entities)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        public async Task<int> ExecuteAsync()
        {
            return await _repository.Context.Connection.OpenDatabase(async () =>
            {
                int count = 0;
                SqlDeleteBuilder<TEntity> builder = new SqlDeleteBuilder<TEntity>(_repository);

                foreach (TEntity entity in _entities)
                {
                    SqlCommand cmd = _repository.Context.Connection.CreateCommand();
                    cmd.CommandText = builder.Build(entity, out DbParameter[] parameters);
                    cmd.Parameters.AddRange(parameters.OfType<SqlParameter>().ToArray());
                    count += await cmd.ExecuteNonQueryAsync();
                }

                return count;
            });
        }
    }
}