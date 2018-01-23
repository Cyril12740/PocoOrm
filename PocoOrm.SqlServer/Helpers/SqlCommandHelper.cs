using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PocoOrm.SqlServer.Helpers
{
    internal static class SqlCommandHelper
    {
        public static async Task<T> OpenDatabase<T>(this SqlCommand cmd, Func<Task<T>> execute)
        {
            try
            {
                await cmd.Connection.OpenAsync();
                return await execute();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

    }
}
