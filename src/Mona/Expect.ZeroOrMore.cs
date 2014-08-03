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
        /// Creates a parser that expects zero or more occurrences of the nested parser to succeed
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parser"></param>
        /// <param name="parseSelector"></param>
        /// <returns></returns>
        public static IParser<TInput, TResultNode> ZeroOrMore<TInput, TNode, TResultNode>(
            this IParser<TInput, TNode> parser, 
            Func<IEnumerable<TInput>, IEnumerable<IParse<TInput, TNode>>, 
            IParse<TInput, TResultNode>> parseSelector)
        {
            return Create<TInput, TResultNode>(
                parse: input => {
                    
                    List<IParse<TInput, TNode>> nestedParses = 
                        new List<IParse<TInput,TNode>>();

                    IParse<TInput, TNode> nestedParse = null;
                    
                    while(true)
                    {
                        nestedParse = parser.Parse(input);
                        if (nestedParse.Succeeded())
                        {
                            nestedParses.Add(nestedParse);
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    return parseSelector(input, nestedParses);
                }
            );
        }

        /// <summary>
        /// Creates a parser that expects zero or more occurrences of the nested parser to succeed
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parser"></param>
        /// <param name="nodeSelector"></param>
        /// <returns></returns>
        public static IParser<TInput, TResultNode> ZeroOrMore<TInput, TNode, TResultNode>(
            this IParser<TInput, TNode> parser,
            Func<IEnumerable<TNode>, TResultNode> nodeSelector)
        {
            return ZeroOrMore<TInput, TNode, TResultNode>(
                parser: parser,
                parseSelector: (input, nestedParses) =>
                {
                    var remainder = input;
                    var lastParse = nestedParses.LastOrDefault();
                    if (lastParse != null)
                    {
                        remainder = lastParse.Remainder;
                    }
                    return new Parse<TInput, TResultNode>(
                        nodeSelector(nestedParses.Select(nestedParse => nestedParse.Node)),
                        remainder, null);
                });
        }

        /// <summary>
        /// Creates a parser that expects zero or more occurrences of the nested parser to succeed
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static IParser<TInput, IEnumerable<TNode>> ZeroOrMore<TInput, TNode>(
            this IParser<TInput, TNode> parser)
        {
            return ZeroOrMore<TInput, TNode, IEnumerable<TNode>>(
                parser: parser,
                parseSelector: (input, nestedParses) =>
                {
                    var remainder = input;
                    var lastParse = nestedParses.LastOrDefault();
                    if (lastParse != null)
                    {
                        remainder = lastParse.Remainder;
                    }
                    return new Parse<TInput, IEnumerable<TNode>>(
                        nestedParses.Select(nestedParse => nestedParse.Node),
                        remainder, null);
                });
        }
        
    }
}
