﻿using System;

namespace PocoOrm.Core.Annotations
{
    public class TableAttribute : Attribute
    {
        public string Name { get; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}