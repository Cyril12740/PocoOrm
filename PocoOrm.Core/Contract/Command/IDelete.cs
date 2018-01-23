namespace PocoOrm.Core.Contract.Command
{
    public interface IDelete<in TEntity> : IExecutable<int> where TEntity : class, new() //todo : , IParameterCounter ?
    {
        IDelete<TEntity> Delete(TEntity entity);

        IDelete<TEntity> Delete(params TEntity[] entities);
    }
}