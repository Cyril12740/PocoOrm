using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using PocoOrm.Core.Annotations;
using PocoOrm.Core.Contract;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Exceptions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core
{
    public class UpdateBuilder<TEntity> where TEntity : class, new()
    {
        private readonly TableInformation<TEntity> _information;
        private readonly IParameterBuilder _builder;
        private readonly IParameterCounter _counter;

        public UpdateBuilder(IRepository<TEntity> repository): this(repository.Information, repository.Context.Options.ParameterBuilder, new ParameterCounter())
        {

        }

        public UpdateBuilder(TableInformation<TEntity> information, IParameterBuilder builder, IParameterCounter counter)
        {
            _information = information ?? throw new ArgumentNullException(nameof(information));
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _counter = counter ?? throw new ArgumentNullException(nameof(counter));
        }

        public ResultUpdateBuilder Prepare(TEntity entity)
        {
            var parameters = new List<DbParameter>();
            var setter = new List<string>();

            if (_information.PrimaryKey == null)
            {
                throw new NoPrimaryKeyException();
            }


            string parameterName = _counter.ParameterName;
            string where = $"{_information.PrimaryKey.Name} = {parameterName}";
            parameters.Add(_builder.Build(parameterName, _information.PrimaryKey, entity));
            
            foreach (var info in _information.Columns)
            {
                if (info.IsIdentity)
                {
                    continue;
                }

                parameterName = _counter.ParameterName;
                string str = $"{info.Name} = {parameterName}";
                setter.Add(str);
                parameters.Add(_builder.Build(parameterName, info, entity));
            }

            //todo ID => Primary key in where clause

            return new ResultUpdateBuilder()
            {
                Sql = string.Join(", ", setter),
                Parameters = parameters,
                Where = where
            };
        }
    }

    public struct ResultUpdateBuilder
    {
        public IEnumerable<DbParameter> Parameters { get; set; }
        public string Sql { get; set; }
        public string Where { get; set; }
    }
}