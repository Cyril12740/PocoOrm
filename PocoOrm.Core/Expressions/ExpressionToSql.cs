using System;
using System.Linq;
using System.Linq.Expressions;
using PocoOrm.Core.Contract.Expressions;
using PocoOrm.Core.Helpers;

namespace PocoOrm.Core.Expressions
{
    public class ExpressionToSql
    {
        private int _parameter;

        public int Parameter => ++_parameter;

        public Options Options { get; }

        public ExpressionToSql(Options options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }


        public ISqlBuilder Visit(Expression expression)
        {
            foreach (IParser parser in Options.ExpressionParser.Where(parser => expression.Is(parser.Type)))
            {
                return parser.Visit(expression, this);
            }
            throw new ArgumentOutOfRangeException(nameof(expression),
                                                  $"type {expression.GetType().Name} is not supported");
        }
    }
}