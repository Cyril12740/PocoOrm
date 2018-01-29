using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocoOrm.Core;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;
using PocoOrm.SqlServer.Command;

namespace PocoOrm.SqlServer
{
    internal class SqlRepository<TEntity> : Repository<TEntity> where TEntity : class, new()
    {
        internal new SqlContext Context => base.Context as SqlContext;

        public SqlRepository(SqlContext context): base(context)
        {
        }

        public override IInsert<TEntity> Insert()
        {
            return new SqlInsert<TEntity>(this);
        }

        public override ISelect<TEntity> Select()
        {
            return new SqlSelect<TEntity>(this);
        }
        
        public override IUpdate<TEntity> Update(params TEntity[] entities)
        {
            return new SqlUpdate<TEntity>(this, entities);
        }

        public override IExecutable<int> Delete(params TEntity[] entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (entities.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(entities));
            }

            return new SqlDelete<TEntity>(this, entities);
        }

        public override IDelete<TEntity> Delete()
        {
            throw new NotImplementedException();
        }
    }

    internal class SqlUpdate<TEntity> : IUpdate<TEntity> where TEntity: class , new ()
    {
        private readonly SqlRepository<TEntity> _repository;
        private TEntity[] _entities;
        private UpdateBuilder<TEntity> _updateBuilder;
        private int _parameterCounter;
        public string ParameterName => $"@parameter{ ++_parameterCounter }";
        public SqlUpdate(SqlRepository<TEntity> repository, TEntity[] entities)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));

            if (entities.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(entities));
            }

            _updateBuilder = new UpdateBuilder<TEntity>(_repository);
        }

        public Options Options => _repository.Context.Options;
        public IUpdate<TEntity> Entities(params TEntity[] entities)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (entities.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(entities));
            }

            _entities = _entities.Concat(entities).ToArray();

            return this;
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            SqlConnection connection = (SqlConnection) _repository.Context.Connection;
            await connection.OpenAsync();

            var updated = new List<TEntity>();
            foreach (TEntity entity in _entities)
            {
                updated.Add(Update(connection, entity));
            }

            return new List<TEntity>();
        }

        private TEntity Update(SqlConnection connection, TEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ").AppendLine(_repository.Information.Name);
            sb.Append("SET ").AppendLine("todo throw an sqlerror");
            sb.Append("WHERE ").AppendLine("primarey key where");

            throw new NotImplementedException();
        }
    }
}