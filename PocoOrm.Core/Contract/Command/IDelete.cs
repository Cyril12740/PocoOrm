using System;
using System.Linq.Expressions;

namespace PocoOrm.Core.Contract.Command
{
    public interface IDelete<TEntity> : IExecutable<int> where TEntity : class, new()//todo : , IParameterCounter ?
    {
        IDelete<TEntity> Where(Expression<Predicate<TEntity>> expression);
    }
}