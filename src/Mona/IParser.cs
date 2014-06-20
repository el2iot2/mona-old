using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// Represents the ability to parse a stream of input and yield a tree, bound with a single local node
    /// </summary>
    /// <typeparam name="TInput">The type of the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting tree node</typeparam>
    public interface IParser<TInput, TNode>
    {
        /// <summary>
        /// Parse as much of the input stream as possible, and return the result as a "Parse"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IObservable<IParse<TInput, TNode>> Parse(IObservable<TInput> input);
    }
}
