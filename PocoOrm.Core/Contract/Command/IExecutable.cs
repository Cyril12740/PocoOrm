using System.Threading.Tasks;

namespace PocoOrm.Core.Contract.Command
{
    public interface IExecutable<TResult>
    {
        Task<TResult> ExecuteAsync();
    }
}