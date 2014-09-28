using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
    /// <summary>
    /// Extension methods for IParse
    /// </summary>
    public static class Parse
    {
        /// <summary>
        /// Creates a new parse object, but with the specified error
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="parse"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IParse<TInput, TNode> WithError<TInput, TNode>(this IParse<TInput, TNode> parse, Exception exception)
        {
            return new Parse<TInput, TNode>(parse.Node, parse.Remainder, exception);
        }

        /// <summary>
        /// Creates a new parse object based on the original, but with a new node type and value
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TResultNode"></typeparam>
        /// <param name="parse"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IParse<TInput, TResultNode> WithNode<TInput, TNode, TResultNode>(this IParse<TInput, TNode> parse, TResultNode node)
        {
            return new Parse<TInput, TResultNode>(node, parse.Remainder, null);
        }


        /// <summary>
        /// Indicates if the parse was a failure
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting node</typeparam>
        /// <param name="parse">The parse result</param>
        /// <returns></returns>
        public static bool Failed<TInput, TNode>(this IParse<TInput, TNode> parse)
        {
            return parse.Error != null;
        }

        /// <summary>
        /// Indicates if the parse was a success
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting node</typeparam>
        /// <param name="parse">The parse result</param>
        /// <returns></returns>
        public static bool Succeeded<TInput, TNode>(this IParse<TInput, TNode> parse)
        {
            return parse.Error == null;
        }
    }
}
