using System;
using System.Data;

namespace PocoOrm.Core.Annotations
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; }

        public DbType Type { get; }

        public int? Size { get; }

        public ColumnAttribute(string name, DbType type, int? size)
        {
            Name = name;
            Type = type;
            Size = size;
        }

        public ColumnAttribute(string name, DbType type) : this(name, type, null)
        {
        }
    }
}