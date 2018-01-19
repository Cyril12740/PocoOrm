using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Contract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IInsert<TEntity> Insert();

        ISelect<TEntity> Select();

        IUpdate<TEntity> Update();

        IDelete<TEntity> Delete();
    }
}