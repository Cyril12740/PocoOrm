using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using PocoOrm.Core.Annotations;

namespace PocoOrm.Core.Command
{
    public class InsertBuilder
    {
        private int _counter;

        private string ParameterName => $"@parameter{++_counter}";

        public InsertBuilderResult Build<TEntity>(Options options, TEntity[] entities)
        {
            Type type = typeof(TEntity);
            Dictionary<PropertyInfo, ColumnAttribute> properties = type.GetProperties()
                                                                       .Where(PredicateMustInsert)
                                                                       .ToDictionary(e => e,
                                                                                     c => c
                                                                                         .GetCustomAttribute<
                                                                                             ColumnAttribute>());
            List<DbParameter> parameters = new List<DbParameter>();
            List<string> entitySql = new List<string>();
            foreach (TEntity entity in entities)
            {
                List<string> paramterNames = new List<string>();
                foreach (KeyValuePair<PropertyInfo, ColumnAttribute> pair in properties)
                {
                    PropertyInfo property = pair.Key;
                    ColumnAttribute column = pair.Value;
                    string parameterName = ParameterName;
                    DbParameter parameter =
                        options.ParameterBuilder.Build(parameterName, column, property.GetValue(entity));
                    paramterNames.Add(parameterName);
                    parameters.Add(parameter);
                }
                entitySql.Add($"({string.Join(", ", paramterNames)})");
            }
            return new InsertBuilderResult
            {
                Columns = $"({string.Join(", ", properties.Select(c => c.Value.Name))})",
                Sql = string.Join("," + Environment.NewLine, entitySql),
                Parameters = parameters
            };
        }

        private static bool PredicateMustInsert(PropertyInfo property)
        {
            ColumnAttribute customAttribute = property.GetCustomAttribute<ColumnAttribute>();
            if (customAttribute is null)
            {
                return false;
            }
            PrimaryKeyAttribute primaryKeyAttribute = property.GetCustomAttribute<PrimaryKeyAttribute>();
            return primaryKeyAttribute?.Identity != true;
        }
    }
}