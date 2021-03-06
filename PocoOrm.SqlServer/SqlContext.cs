﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PocoOrm.Core;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Expressions;

namespace PocoOrm.SqlServer
{
    public abstract class SqlContext : GenericContext
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        protected SqlContext(SqlConnection connection, Options options) : base(connection, options)
        {
            this.Options.Use(new SqlParameterBuilder());
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

    internal class SqlParameterBuilder : ParameterBuilder<SqlParameter>
    {
        protected override SqlParameter Build(string name, ColumnAttribute column, object value)
        {
            return column.Size != null
                       ? new SqlParameter()
                       {
                           ParameterName = name,
                           DbType = column.Type,
                           Value = value,
                           Size = column.Size.Value
                       }
                       : new SqlParameter()
                       {
                           ParameterName = name,
                           DbType = column.Type,
                           Value = value,
                       };
        }
    }
}