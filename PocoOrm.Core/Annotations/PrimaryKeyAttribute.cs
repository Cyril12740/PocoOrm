using System;

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
}