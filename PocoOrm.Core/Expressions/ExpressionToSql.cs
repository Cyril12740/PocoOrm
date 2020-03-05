using System;
using System.Linq;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Command;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions
{
    public class ExpressionToSql
    {
        public IParameterCounter Counter { get; }

        public Options Options { get; }

        public ExpressionToSql(IParameterCounter counter, Options options)
        {
            Counter = counter ?? throw new ArgumentNullException(nameof(counter));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ISqlBuilder Visit(Expression expression)
        {
            foreach (IParser parser in Options.Parser.Where(parser => expression.Is(parser.Type)))
            {
                return parser.Visit(expression, this);
            }

            throw new ArgumentOutOfRangeException(nameof(expression),
                                                  $"type {expression.GetType().Name} is not supported");
        }
    }
}