using System.Collections.Generic;

namespace PocoOrm.Core.Contract.Command
{
    public interface IUpdate<TEntity> : IExecutable<IEnumerable<TEntity>>, IParameterCounter where TEntity : class, new()
    {
        Options Options { get; }
        IUpdate<TEntity> Entities(params TEntity[] entities);
    }

    public interface IParameterCounter
    {
        /// <summary>
        /// Must be introduce an different name on each call
        /// </summary>
        string ParameterName { get; }
    }
}