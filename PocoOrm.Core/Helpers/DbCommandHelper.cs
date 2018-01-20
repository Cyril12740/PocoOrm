using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PocoOrm.Core.Helpers
{
    public static class DbCommandHelper
    {
        public static async Task<IEnumerable<TEntity>> ExecuteReaderAsync<TEntity>(this IDbCommand cmd,
                                                                                   Mapper<TEntity> mapper)
            where TEntity : class, new()
        {
            return await Task.Run(() => {
                cmd.Connection.Open();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    List<TEntity> items = new List<TEntity>();

                    while (reader.Read())
                    {
                        items.Add(mapper.Map(reader));
                    }

                    cmd.Connection.Close(); //todo not like this - finaly block
                    return items;
                }
            });
        }
    }
}