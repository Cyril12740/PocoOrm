using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Exceptions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Command
{
    public abstract class ReaderExecute<TEntity> : IExecutable<IEnumerable<TEntity>> where TEntity : class, new()
    {
        protected IRepository<TEntity> Repository { get; }
        protected ReaderExecute(IRepository<TEntity> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected async Task<IEnumerable<TEntity>> ExecuteReaderAsync(IDbCommand cmd)
        {
            return await Task.Run(() => {
                try
                {
                    Intersept(cmd);

                    cmd.Connection.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        List<TEntity> items = new List<TEntity>();

                        while (reader.Read())
                        {
                            items.Add(Repository.Mapper.Map(reader));
                        }

                        return items;
                    }
                }
                finally
                {
                    if(cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Close();
                    }
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