using System;
using System.Data.Common;
using System.Text;
using PocoOrm.Core.Contract;

namespace PocoOrm.SqlServer
{
    internal class SqlDeleteBuilder<TEntity> where TEntity : class, new()
    {
        private int _counter;
        public string ParameterName => $"@parameter{++_counter}";

        private readonly IRepository<TEntity> _repository;

        public SqlDeleteBuilder(IRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public string Build(TEntity entity, out DbParameter[] sqlParameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM ").AppendLine(_repository.Information.Name);

            if (_repository.Information.PrimaryKey == null)
            {
                throw new Exception("No primarey keyy defined");
            }

            string paramterName = ParameterName;

            sqlParameters = new[]
            {
                _repository.Context.Options.ParameterBuilder.Build(paramterName, _repository.Information.PrimaryKey, entity)
            };

            return sb.ToString();
        }
        
    }
}