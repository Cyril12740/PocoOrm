using System;
using PocoOrm.Core;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;
using PocoOrm.SqlServer.Command;

namespace PocoOrm.SqlServer
{
    internal class SqlRepository<TEntity> : GenericRepository<TEntity> where TEntity : class, new()
    {
        internal IContext Context { get; }

        public SqlRepository(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override IInsert<TEntity> Insert()
        {
            return new SqlInsert<TEntity>();
        }

        public override ISelect<TEntity> Select()
        {
            return new SqlSelect<TEntity>(this);
        }

        public override IUpdate<TEntity> Update()
        {
            throw new NotImplementedException();
        }

        public override IDelete<TEntity> Delete()
        {
            throw new NotImplementedException();
        }
    }
}