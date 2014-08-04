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
        /// <param name="parser"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IParser<TInput, TNode> Select<TInput, TNode>(this IParser<TInput, TNode> parser, Func<IParse<TInput, TNode>, IParse<TInput, TNode>> selector)
        {
            return Create<TInput, TNode>(
                input =>
                {
                    var parse = parser.Parse(input);
                    return selector(parse);
                });
        }
    }
}
