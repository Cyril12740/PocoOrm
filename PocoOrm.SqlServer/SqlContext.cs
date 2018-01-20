using System;
using System.Data.SqlClient;
using System.Linq;
using PocoOrm.Core;

namespace PocoOrm.SqlServer
{
    public abstract class SqlContext : GenericContext
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        protected SqlContext(SqlConnection connection, Options options) : base(connection, options)
        {
            Options.Use(new SqlParameterBuilder());
        }

        protected sealed override object CreateRepository(Type genericTable)
        {
            Type repositorytype = typeof(SqlRepository<>);
            Type result = repositorytype.MakeGenericType(genericTable);
            return result.GetConstructors()
                         .First()
                         .Invoke(new object[]
                         {
                             this
                         });
        }
    }
}