using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Command
{
    public class InsertBuilder<TEntity> where TEntity : class, new()
    {
        private readonly Repository<TEntity> _repository;
        private readonly IParameterCounter _counter;

        public InsertBuilder(Repository<TEntity> repository, IParameterCounter counter)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _counter = counter ?? throw new ArgumentNullException(nameof(counter));
        }

        public InsertBuilderResult Build(Options options, TEntity[] entities)
        {                                                                     /*   ColumnAttribute>());*/
            List<DbParameter> parameters = new List<DbParameter>();
            List<string> entitySql = new List<string>();

            foreach (TEntity entity in entities)
            {
                List<string> paramterNames = new List<string>();

                foreach (ColumnInformation<TEntity> column in _repository.Information.Columns)
                {
                    if (column.IsIdentity)
                    {
                        continue;
                    }

                    string parameterName = _counter.ParameterName;
                    DbParameter parameter = options.ParameterBuilder.Build(parameterName, column, entity);
                    paramterNames.Add(parameterName);
                    parameters.Add(parameter);
                }

                entitySql.Add($"({string.Join(", ", paramterNames)})");
            }

            return new InsertBuilderResult
            {
                Columns = $"({string.Join(", ", _repository.Information.Columns.Where(c => !c.IsIdentity).Select(c => c.Name))})",
                Sql = string.Join("," + Environment.NewLine, entitySql),
                Parameters = parameters
            };
        }
    }
}