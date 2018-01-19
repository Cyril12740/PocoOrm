using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PocoOrm.Core.Contract.Command
{
    public interface ISelect<TEntity> : IExecutable<IEnumerable<TEntity>> where TEntity : class, new()
    {
        ISelect<TEntity> Where(Expression<Predicate<TEntity>> expression);

        ISelect<TEntity> OderBy(Expression<Func<TEntity, object>> expression, bool desc = false);
    }
}