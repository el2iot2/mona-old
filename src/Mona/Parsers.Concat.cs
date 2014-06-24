using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// All basic parser generators and extension methods for combining them
    /// </summary>
    public static partial class Parsers
    {
        /// <summary>
        /// Creates a parser that is a concatenation of two other parsers
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TNode1"></typeparam>
        /// <typeparam name="TNode2"></typeparam>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static IParser<TInput, Tuple<IParser<TInput, TNode1>, IParser<TInput, TNode2>>> Concat<TInput, TNode1, TNode2>(IParser<TInput, TNode1> prefix, IParser<TInput, TNode1> suffix)
        {
            return Create<TInput, Tuple<IParser<TInput, TNode1>, IParser<TInput, TNode2>>>(
                parseFunc => null);
        }

        
    }
}
