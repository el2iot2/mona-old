using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// Represents the result of a parser's work
    /// </summary>
    /// <typeparam name="TInput">The type of the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting tree node</typeparam>
    public interface IParse<TInput, TNode>
    {
        /// <summary>
        /// The root/local node of the resulting tree
        /// </summary>
        TNode Node
        {
            get;
        }

        /// <summary>
        /// The remaining Input, if any
        /// </summary>
        IObservable<TInput> Remainder
        {
            get;
        }

        /// <summary>
        /// The exception, if one occurred. Null otherwise
        /// </summary>
        Exception Error
        {
            get;
        }
    }

    /// <summary>
    /// Extension types for IParse
    /// </summary>
    public static class ParseExtensions
    {
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
