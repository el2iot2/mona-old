using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
    /// <summary>
    /// Extension methods for creating and adapting IParser instances
    /// </summary>
    public static partial class Parser
    {
        /// <summary>
        /// Explicitly create a parser via a parse func
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting node</typeparam>
        /// <returns></returns>
        public static IParser<TInput, TNode> Create<TInput, TNode>(Func<IEnumerable<TInput>, IParse<TInput, TNode>> parse)
        {
            return new Parser<TInput, TNode>(parse: parse);
        }

        /// <summary>
        /// Helper to parse a simple string of characters
        /// </summary>
        /// <typeparam name="TNode">The type of the resulting tree node</typeparam>
        /// <param name="parser">The parser</param>
        /// <param name="input">The input string</param>
        /// <returns>The resulting parse</returns>
        public static IParse<char, TNode> Parse<TNode>(this IParser<char, TNode> parser, string input)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }
            return parser.Parse(input.ToCharArray());
        }
    }
}
