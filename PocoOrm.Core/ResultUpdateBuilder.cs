using System.Collections.Generic;
using System.Data.Common;

namespace PocoOrm.Core
{
    public struct ResultUpdateBuilder
    {
        public IEnumerable<DbParameter> Parameters { get; set; }
        public string Sql { get; set; }
        public string Where { get; set; }
    }
}