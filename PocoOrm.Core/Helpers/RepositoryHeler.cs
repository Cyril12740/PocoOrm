using System.Collections.Generic;
using System.Linq;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Helpers
{
    public static class RepositoryHeler
    {
        public static IExecutable<IEnumerable<TEntity>> Insert<TEntity>(this IRepository<TEntity> repository,
                                                                        params TEntity[] entities)
            where TEntity : class, new()
        {
            return repository.Insert().Values(entities);
        }

        public static IExecutable<IEnumerable<TEntity>> Insert<TEntity>(this IRepository<TEntity> repository,
                                                                        IEnumerable<TEntity> entities)
            where TEntity : class, new()
        {
            return repository.Insert().Values(entities.ToArray());
        }
    }
}