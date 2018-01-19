using System.Data;

namespace PocoOrm.Core.Contract
{
    public interface IContext
    {
        Options Options { get; }

        IDbConnection Connection { get; }
    }
}