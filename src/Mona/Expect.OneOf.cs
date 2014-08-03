using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    public static partial class Expect
    {
        /// <summary>
        /// Creates a parser that tries each component parser in turn, failing if none parses successfully
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="parsers"></param>
        /// <returns></returns>
        public static IParser<TInput, TNode> OneOf<TInput, TNode>(params IParser<TInput, TNode>[] parsers)
        {
            return Create<TInput, TNode>(
                parse: input => {
                    var parseErrors = new List<Exception>();
                    foreach (var parser in parsers)
                    {
                        var parse = parser.Parse(input);
                        if (parse.Succeeded())
                        {
                            return parse;
                        }
                        else
                        {
                            parseErrors.Add(parse.Error);
                        }
                    }
                    return new Parse<TInput, TNode>(default(TNode), input, new AggregateException(parseErrors));

                }
            );
        }
    }
}
