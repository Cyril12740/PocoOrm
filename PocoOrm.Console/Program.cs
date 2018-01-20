using System.Data.SqlClient;
using PocoOrm.Core;

namespace PocoOrm.Console
{
    internal class Program
    {
        private static void Main()
        {
            Context unused = new Context(new SqlConnection(), new Options());
        }
    }
}