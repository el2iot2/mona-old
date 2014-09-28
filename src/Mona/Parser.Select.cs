using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
    public static partial class Parser
    {
        /// <summary>
        /// Given a parser, selects a variation based on applying the selector to the resulting parse
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parser"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IParser<TInput, TResultNode> Select<TInput, TNode, TResultNode>(this IParser<TInput, TNode> parser, Func<IParse<TInput, TNode>, IParse<TInput, TResultNode>> selector)
        {
            return Create<TInput, TResultNode>(
                input =>
                {
                    var parse = parser.Parse(input);
                    return selector(parse);
                });
        }

        /// <summary>
        /// Given a parser, selects a variation based on applying the selector to the resulting parse node
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parser"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IParser<TInput, TResultNode> SelectNode<TInput, TNode, TResultNode>(this IParser<TInput, TNode> parser, Func<TNode, TResultNode> selector)
        {
            return Create<TInput, TResultNode>(
                input =>
                {
                    var parse = parser.Parse(input);
                    return parse.WithNode(selector(parse.Node));
                });
        }
    }
}
