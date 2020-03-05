using System.Data;

namespace PocoOrm.Core
{
    public interface IInterceptCommand
    {
        void Intercept(IDbCommand command);
    }
}