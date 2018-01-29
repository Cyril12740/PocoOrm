using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PocoOrm.SqlServer.Helpers
{
    internal static class SqlConnectionHelper
    {
        public static async Task<T> OpenDatabase<T>(this SqlConnection connection, Func<Task<T>> execute)
        {
            try
            {
                await connection.OpenAsync();
                return await execute();
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
