using System.Collections.Generic;

namespace PocoOrm.Core.Contract.Command
{
    public interface IInsert<TEntity> : IExecutable<IEnumerable<TEntity>> where TEntity : class, new()
    {
        IInsert<TEntity> Values(params TEntity[] values);
    }
}