using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PocoOrm.Core.Helpers
{
    public static class DbCommandHelper
    {
        public static async Task<T> OpenDatabase<T>(this IDbConnection connection, Func<Task<T>> execute)
        {
            try
            {
                connection.Open();
                return await execute();
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<IEnumerable<TEntity>> ExecuteReaderAsync<TEntity>(this IDbCommand cmd,
                                                                                   Mapper<TEntity> mapper)
            where TEntity : class, new()
        {
            return await Task.Run(() => {
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    List<TEntity> items = new List<TEntity>();

                    while (reader.Read())
                    {
                        items.Add(mapper.Map(reader));
                    }
                    return items;
                }
            });
        }
    }
}