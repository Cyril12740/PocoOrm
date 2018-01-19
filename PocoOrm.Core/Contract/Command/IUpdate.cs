using System.Collections.Generic;

namespace PocoOrm.Core.Contract.Command
{
    public interface IUpdate<TEntity> : IExecutable<IEnumerable<TEntity>> where TEntity : class, new()
    {
        IUpdate<TEntity> Entity(TEntity entity);

        IUpdate<TEntity> Entities(params TEntity[] entities);
    }
}