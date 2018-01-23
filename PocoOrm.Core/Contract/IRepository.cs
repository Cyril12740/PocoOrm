using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Contract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IContext Context { get; }
        
        Mapper<TEntity> Mapper { get; }
        
        TableInformation<TEntity> Information { get; }

        IInsert<TEntity> Insert();

        ISelect<TEntity> Select();
        
        IUpdate<TEntity> Update(params TEntity[] entities);

        IExecutable<int> Delete(params TEntity[] entities);

        IDelete<TEntity> Delete();
    }
}