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
    public static partial class Expect
    {
        /// <summary>
        /// Explicitly create a parser via a parse func
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting node</typeparam>
        /// <returns></returns>
        public static IParser<TInput, TNode> Create<TInput, TNode>(Func<IObservable<TInput>, IObserver<IParse<TInput, TNode>>, IDisposable> parse)
        {
            return new Parser<TInput, TNode>(parse: parse);
        }

        /// <summary>
        /// Explicitly create a parser via a parse func
        /// </summary>
        /// <typeparam name="TInput">The type of the input symbol</typeparam>
        /// <typeparam name="TNode">The type of the resulting node</typeparam>
        /// <returns></returns>
        public static IParser<TInput, TNode> Create<TInput, TNode>(Func<IObservable<TInput>, IObserver<IParse<TInput, TNode>>, Task<IDisposable>> parseAsync)
        {
            return new AsyncParser<TInput, TNode>(parseAsync: parseAsync);
        }
    }
}
