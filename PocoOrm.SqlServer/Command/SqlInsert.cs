using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocoOrm.Core.Command;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Helpers;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlInsert<TEntity> : IInsert<TEntity> where TEntity : class, new()
    {
        private readonly SqlRepository<TEntity> _repository;

        private TEntity[] _entities = new TEntity[0];

        public SqlInsert(SqlRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            InsertBuilderResult result = new InsertBuilder<TEntity>(_repository).Build(_repository.Context.Options, _entities);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ")
              .AppendLine(_repository.Information.Name)
              .AppendLine(result.Columns)
              .AppendLine("OUTPUT INSERTED.*")
              .AppendLine("VALUES")
              .AppendLine(result.Sql);
            SqlCommand cmd = _repository.Context.Connection.CreateCommand();
            cmd.CommandText = sb.ToString();

            foreach (DbParameter parameter in result.Parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            return await cmd.Connection.OpenDatabase(async () => await cmd.ExecuteReaderAsync(_repository.Mapper));
        }

        public IInsert<TEntity> Values(params TEntity[] values)
        {
            _entities = _entities.Concat(values).ToArray();
            return this;
        }
    }
}