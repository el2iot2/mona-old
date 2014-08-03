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
        /// Creates a parser that makes the nested parser optional
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parser"></param>
        /// <param name="parseSelector"></param>
        /// <returns></returns>
        public static IParser<TInput, TResultNode> Optional<TInput, TNode, TResultNode>(IParser<TInput, TNode> parser, Func<IParse<TInput, TNode>, IParse<TInput, TResultNode>> parseSelector)
        {
            return Create<TInput, TResultNode>(
                parse: input => {
                    var optionalParse = parser.Parse(input);
                    return parseSelector(optionalParse);
                }
            );
        }

        /// <summary>
        /// Creates a parser that makes the nested parser optional
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parser"></param>
        /// <param name="nodeSelector"></param>
        /// <returns></returns>
        public static IParser<TInput, TResultNode> Optional<TInput, TNode, TResultNode>(IParser<TInput, TNode> parser, Func<TNode, TResultNode> nodeSelector)
        {
            return Optional<TInput, TNode, TResultNode>(
                parser: parser,
                parseSelector: optionalParse => 
                    new Parse<TInput, TResultNode>(nodeSelector(optionalParse.Node), optionalParse.Remainder, null)
                    );
        }
    }
}
