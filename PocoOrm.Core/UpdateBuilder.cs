using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core
{
    public class UpdateBuilder<TEntity> where TEntity : class, new()
    {
        private readonly IUpdate<TEntity> _updateCommand;
        private readonly Dictionary<ColumnAttribute, PropertyInfo> _columnToUpdate;
        public UpdateBuilder(IUpdate<TEntity> updateCommand)
        {
            _updateCommand = updateCommand ?? throw new ArgumentNullException(nameof(updateCommand));
            var type = typeof(TEntity);

            _columnToUpdate = type.GetProperties()
                                  .Where(p => p.GetCustomAttribute<ColumnAttribute>() != null && 
                                              !(p.GetCustomAttribute<PrimaryKeyAttribute>()?.Identity ?? false))
                                  .ToDictionary(p => p.GetCustomAttribute<ColumnAttribute>(), p => p);
        }

        ResultUpdateBuilder Prepare(TEntity entity)
        {
            var parameters = new List<DbParameter>();
            var setter = new List<string>();
            foreach (KeyValuePair<ColumnAttribute, PropertyInfo> info in _columnToUpdate)
            {
                string parameterName = _updateCommand.ParameterName;
                var str = $"{info.Key.Name} = {parameterName}";
                setter.Add(str);
                parameters.Add(_updateCommand.Options.ParameterBuilder.Build(parameterName, info.Key, info.Value.GetValue(entity)));
            }

            //todo ID => Primary key in where clause

            return new ResultUpdateBuilder()
            {
                Sql = string.Join(", ", setter),
                Parameters = parameters
            };
        }
    }

    public struct ResultUpdateBuilder
    {
        public IEnumerable<DbParameter> Parameters { get; set; }
        public string Sql { get; set; }
    }
}