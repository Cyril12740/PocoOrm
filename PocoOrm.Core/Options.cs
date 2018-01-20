using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;
using PocoOrm.Core.Expressions.Parser;

namespace PocoOrm.Core
{
    public class Options
    {
        private readonly List<IBinaryParser> _binaryParser = new List<IBinaryParser>();

        private readonly List<IParser> _parser = new List<IParser>();
        public static    Options       Empty => new Options();

        public static Options Default => new Options()
                                         .Use(new GroupedConditionParser(), new ColumnValueParser(), new CompareNullParser())
                                         .Use(Expressions.Parser.Parser.Default());

        public ReadOnlyCollection<IParser> Parser => _parser.AsReadOnly();

        public ReadOnlyCollection<IBinaryParser> BinaryParser => _binaryParser.AsReadOnly();

        public IInterceptCommand InterceptCommands { get; private set; }

        public IParameterBuilder ParameterBuilder { get; private set; }

        private Options()
        {
        }

        private Options Use(IEnumerable<IParser> parameterBuilder)
        {
            return Use(parameterBuilder.ToArray());
        }

        public Options Use(IParameterBuilder parameterBuilder)
        {
            ParameterBuilder = parameterBuilder ?? throw new ArgumentNullException(nameof(parameterBuilder));
            return this;
        }

        public Options Use(IInterceptCommand interceptCommand)
        {
            InterceptCommands = interceptCommand ?? throw new ArgumentNullException(nameof(interceptCommand));
            return this;
        }

        public Options Use(params IParser[] parsers)
        {
            if (parsers is null)
            {
                throw new ArgumentNullException(nameof(parsers));
            }

            _parser.AddRange(parsers);
            return this;
        }

        public Options Use(params IBinaryParser[] binaryBuilders)
        {
            if (binaryBuilders is null)
            {
                throw new ArgumentNullException(nameof(binaryBuilders));
            }

            _binaryParser.AddRange(binaryBuilders);
            return this;
        }

        public Options Verify()
        {
            throw new NotImplementedException();
        }
    }
}