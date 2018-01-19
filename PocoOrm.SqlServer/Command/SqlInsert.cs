using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.SqlServer.Command
{
    internal class SqlInsert<TEntity> : IInsert<TEntity> where TEntity : class, new()
    {
        public Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public IInsert<TEntity> Values(params TEntity[] values)
        {
            throw new NotImplementedException();
        }
    }
}