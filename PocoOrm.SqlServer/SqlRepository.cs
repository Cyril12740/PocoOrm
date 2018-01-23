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
        public SqlRepository(IContext context): base(context)
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

        public override IDelete<TEntity> Delete()
        {
            throw new NotImplementedException();
        }
    }

    internal class SqlUpdate<TEntity> : IUpdate<TEntity> where TEntity: class , new ()
    {
        private readonly IRepository<TEntity> _repository;
        private TEntity[] _entities;
        private UpdateBuilder<TEntity> _updateBuilder;
        private int _parameterCounter;
        public string ParameterName => $"@parameter{ ++_parameterCounter }";
        public SqlUpdate(IRepository<TEntity> repository, TEntity[] entities)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));

            if (entities.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(entities));
            }

            _updateBuilder = new UpdateBuilder<TEntity>(this);
        }

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
            sb.Append("UPDATE ").AppendLine(_repository.TableName);//todo
            sb.Append("SET ").AppendLine("todo throw an sqlerror");
            sb.Append("WHERE ").AppendLine("primarey key where");
        }
    }
}