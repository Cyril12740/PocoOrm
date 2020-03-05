using System.Collections.Generic;
using System.Data.Common;

namespace PocoOrm.Core.Command
{
    public struct InsertBuilderResult
    {
        public string Columns { get; set; }

        public string Sql { get; set; }

        public List<DbParameter> Parameters { get; set; }
    }
}