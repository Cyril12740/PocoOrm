using System;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public IContext Context { get; }
        public  TableInformation<TEntity> Information { get; } = new TableInformation<TEntity>();

        public Mapper<TEntity> Mapper { get; }
        
        protected GenericRepository(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = new Mapper<TEntity>();
        }

        public abstract IInsert<TEntity> Insert();

        public abstract ISelect<TEntity> Select();

        public abstract IUpdate<TEntity> Update();

        public abstract IExecutable<int> Delete(params TEntity[] entities);

        public abstract IDelete<TEntity> Delete();
    }
}