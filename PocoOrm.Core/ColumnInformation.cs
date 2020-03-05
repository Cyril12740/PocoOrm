using System;
using System.Data;
using System.Reflection;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core
{
    public class ColumnInformation<TEntity>
        where TEntity : class, new()
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly PrimaryKeyAttribute _primareyKeyAttribute;

        public ColumnInformation(PropertyInfo property, ColumnAttribute columnAttribute, PrimaryKeyAttribute primareyKeyAttribute)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            _columnAttribute = columnAttribute ?? throw new ArgumentNullException(nameof(columnAttribute));
            _primareyKeyAttribute = primareyKeyAttribute;
        }

        public PropertyInfo Property { get; }

        public string Name => _columnAttribute.Name;
        public DbType Type => _columnAttribute.Type;
        public int? Size => _columnAttribute.Size;
        public bool IsPrimaryKey => _primareyKeyAttribute != null;
        public bool IsIdentity => _primareyKeyAttribute?.Identity ?? false;
        public ColumnAttribute Information => _columnAttribute;

        public object Value(TEntity entity)
        {
            return Property.GetValue(entity);
        }
    }
}