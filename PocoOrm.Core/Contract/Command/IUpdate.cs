using System.Collections.Generic;

namespace PocoOrm.Core.Contract.Command
{
    public interface IUpdate<TEntity> : IExecutable<IEnumerable<TEntity>>, IParameterCounter where TEntity : class, new()
    {
        Options Options { get; }//todo why
        IUpdate<TEntity> Entities(params TEntity[] entities);
    }
}