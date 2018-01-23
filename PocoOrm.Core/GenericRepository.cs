using System;
using System.Collections.Generic;
using System.Reflection;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public Mapper<TEntity> Mapper { get; }

        public IContext Context { get; }
        public string TableName { get; }

        protected Repository(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Type type = typeof(TEntity);
            TableName = type.GetCustomAttribute<TableAttribute>()?.Name ??
                        throw new ArgumentException($"TableAttribute isn't defined on {type.Name}", nameof(TEntity));
            Mapper = new Mapper<TEntity>();
        }

        public abstract IInsert<TEntity> Insert();

        public abstract ISelect<TEntity> Select();
        
        public abstract IUpdate<TEntity> Update(params TEntity[] entities);

        public abstract IDelete<TEntity> Delete();
    }
}