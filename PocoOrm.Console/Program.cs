using System.Data.SqlClient;

namespace PocoOrm.Console
{
    using Core;

    internal class Program
    {
        private static void Main()
        {
            Context unused = new Context(new SqlConnection(), new ContextOptions());
        }
    }
}