using System;
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
}