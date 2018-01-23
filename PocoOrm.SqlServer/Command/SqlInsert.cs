using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocoOrm.Core.Command;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Helpers;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlInsert<TEntity> : ReaderExecute<TEntity>, IInsert<TEntity> where TEntity : class, new()
    {
        private readonly SqlRepository<TEntity> _repository;

        private TEntity[] _entities = new TEntity[0];
        private int _parameterCounter;

        public SqlInsert(SqlRepository<TEntity> repository): base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            if (_entities.Length == 0)
            {
                return new TEntity[0];
            }

            InsertBuilderResult result = new InsertBuilder(this).Build(_repository.Context.Options, _entities);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ")
              .AppendLine(_repository.TableName)
              .AppendLine(result.Columns)
              .AppendLine("OUTPUT INSERTED.*")
              .AppendLine("VALUES")
              .AppendLine(result.Sql);
            IDbCommand cmd = _repository.Context.Connection.CreateCommand();
            cmd.CommandText = sb.ToString();

            foreach (DbParameter parameter in result.Parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            return await ExecuteReaderAsync(cmd);
        }

        public IInsert<TEntity> Values(params TEntity[] values)
        {
            _entities = _entities.Concat(values?.Where(e => e != null) ?? new TEntity[0]).ToArray();
            return this;
        }
        public string ParameterName => $"@parameter{++_parameterCounter}";
    }
}