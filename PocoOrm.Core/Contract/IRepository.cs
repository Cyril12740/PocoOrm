using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Contract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IContext Context { get; }

        TableInformation<TEntity> Information { get; }
        IInsert<TEntity> Insert();

        ISelect<TEntity> Select();

        IUpdate<TEntity> Update();

        IExecutable<int> Delete(params TEntity[] entities);

        IDelete<TEntity> Delete();
    }
}