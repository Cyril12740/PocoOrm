using System;
using System.Data;

namespace PocoOrm.Core.Annotations
{
    public class PrimaryKeyAttribute : Attribute
    {
        public bool Identity { get; }

        public PrimaryKeyAttribute(bool identity)
        {
            Identity = identity;
        }
    }

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