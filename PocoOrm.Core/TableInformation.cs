using System;
using System.Collections.Generic;
using System.Reflection;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core
{
    public class TableInformation<TEntity>
        where TEntity : class, new()
    {
        private List<ColumnInformation<TEntity>> _columns = new List<ColumnInformation<TEntity>>();
        public IReadOnlyList<ColumnInformation<TEntity>> Columns => _columns.AsReadOnly();

        public TableInformation()
        {
            Type type = typeof(TEntity);
            Name = type.GetCustomAttribute<TableAttribute>()?.Name ??
                   throw new ArgumentException($"TableAttribute isn't defined on {type.Name}", nameof(TEntity));
            
            foreach (PropertyInfo property in type.GetProperties())
            {
                ColumnAttribute columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                PrimaryKeyAttribute primareyKeyAttribute = property.GetCustomAttribute<PrimaryKeyAttribute>();
                if (columnAttribute != null)
                {
                    ColumnInformation<TEntity> columnInformation = new ColumnInformation<TEntity>(property, columnAttribute, primareyKeyAttribute);

                    if (columnInformation.IsPrimaryKey)
                    {
                        PrimaryKey = columnInformation;
                    }

                    _columns.Add(columnInformation);
                }
            }

        }

        public ColumnInformation<TEntity> PrimaryKey { get; }

        public string Name { get; }
    }
}