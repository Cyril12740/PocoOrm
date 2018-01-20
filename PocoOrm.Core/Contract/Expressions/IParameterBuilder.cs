using System.Data.Common;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core.Contract.Expressions
{
    public interface IParameterBuilder
    {
        DbParameter Build(string name, ColumnAttribute column, object value);//todo merge into GenericContext & delete IContext ? aussi 
        // todo  connection.CreateCommand().CreateParameter()// => may be
    }
}