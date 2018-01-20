using System.Data.Common;

namespace PocoOrm.Core
{
    public interface IInterceptCommand
    {
        void Intercept(DbCommand command);
    }
}