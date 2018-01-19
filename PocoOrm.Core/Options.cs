using System;
using System.Collections.Generic;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Expressions.Builder;
using PocoOrm.Core.Expressions.Parser;

namespace PocoOrm.Core
{
    public class Options
    {
        private readonly List<IBinaryBuilder> _binaryParser = new List<IBinaryBuilder>();

        private readonly List<IParser> _expressionParser = new List<IParser>();

        public IEnumerable<IParser> ExpressionParser => _expressionParser.AsReadOnly();

        public IEnumerable<IBinaryBuilder> BinaryParser => _binaryParser.AsReadOnly();

        public IParameterBuilder ParameterBuilder { get; private set; }

        public Options()
        {
            Use(new BinaryParser(this), new LambdaParser(), new MemberParser(), new ConstantParser());
            Use(new ColumnValueBuilder());
        }

        public Options Use(IParser parser)
        {
            if (parser != null)
            {
                _expressionParser.Add(parser);
            }

            return this;
        }
        public Options Use(params IParser[] parsers)
        {
            if (parsers is null)
            {
                throw new ArgumentNullException(nameof(parsers));
            }

            _expressionParser.AddRange(parsers);
            return this;
        }

        public Options Use(IBinaryBuilder parser)
        {
            if (parser is null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            _binaryParser.Add(parser);
            return this;
        }

        public Options Use(params IBinaryBuilder[] parsers)
        {
            if (parsers is null)
            {
                throw new ArgumentNullException(nameof(parsers));
            }

            _binaryParser.AddRange(parsers);
            return this;
        }

        public Options Use(IParameterBuilder builder)
        {
            ParameterBuilder = builder ?? throw new ArgumentNullException(nameof(builder));
            return this;
        }

        public Options Verify()
        {
            throw new NotImplementedException();
        }
    }
}