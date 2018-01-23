using System.Collections.Generic;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Contract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IContext Context { get; }

        string TableName { get; }

        Mapper<TEntity> Mapper { get; }

        IInsert<TEntity> Insert();

        ISelect<TEntity> Select();
        
        IUpdate<TEntity> Update(params TEntity[] entities);

        IDelete<TEntity> Delete();
    }
}