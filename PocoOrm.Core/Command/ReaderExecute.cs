using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Exceptions;

namespace PocoOrm.Core.Command
{
    public abstract class ReaderExecute<TEntity> : IExecutable<IEnumerable<TEntity>> where TEntity : class, new()
    {
        protected Repository<TEntity> Repository { get; }
        protected ReaderExecute(Repository<TEntity> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected async Task<IEnumerable<TEntity>> ExecuteReaderAsync(IDbCommand cmd)
        {
            return await Task.Run(() => {
                Intersept(cmd);
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    List<TEntity> items = new List<TEntity>();

                    while (reader.Read())
                    {
                        items.Add(Repository.Mapper.Map(reader));
                    }

                    return items;
                }
            });
        }

        private void Intersept(IDbCommand cmd)
        {
            try
            {
                Repository.Context.Options?.InterceptCommands?.Intercept(cmd);
            }
            catch (Exception e)
            {
                throw new InterseptException(e);
            }
        }

        public abstract Task<IEnumerable<TEntity>> ExecuteAsync();
    }
}