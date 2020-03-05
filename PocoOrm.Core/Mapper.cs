using System;
using System.Data;
using System.Reflection;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core
{
    public class Mapper<TEntity> where TEntity : class, new()
    {
        public TEntity Map(IDataReader reader)
        {
            TEntity entity = new TEntity();

            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                ColumnAttribute customAttribute = property.GetCustomAttribute<ColumnAttribute>();

                if (customAttribute != null)
                {
                    property.SetValue(entity, Value(reader, customAttribute));
                }
            }

            return entity;
        }

        private static object Value(IDataRecord reader, ColumnAttribute column)
        {
            object valueColumn = reader[column.Name];
            return valueColumn != DBNull.Value ? valueColumn : null;
        }
    }
}