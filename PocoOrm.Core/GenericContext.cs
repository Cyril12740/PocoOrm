using System;
using System.Data;
using System.Linq;
using System.Reflection;
using PocoOrm.Core.Contract;

namespace PocoOrm.Core
{
    public abstract class GenericContext : IContext
    {
        protected GenericContext(IDbConnection connection, Options options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            InitialiseRepository();
        }

        public Options Options { get; }

        public IDbConnection Connection { get; }

        private void InitialiseRepository()
        {
            Type type = GetType();

            foreach (PropertyInfo property in type
                                              .GetProperties(BindingFlags.Instance |
                                                             BindingFlags.Public |
                                                             BindingFlags.DeclaredOnly)
                                              .Where(p =>
                                                         p.PropertyType.IsGenericType &&
                                                         p.PropertyType.GetGenericTypeDefinition() ==
                                                         typeof(IRepository<>)))
            {
                object repository = CreateRepository(property.PropertyType.GetGenericArguments()[0]);
                property.SetValue(this, repository);
            }
        }

        protected abstract object CreateRepository(Type genericTable);
    }
}