using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// A simple wrapper around an async parse func that makes up a parser
    /// </summary>
    /// <remarks>
    /// We make this a full type, rather than just a Func so that we can better manage
    /// Composite parsers
    /// </remarks>
    /// <typeparam name="TInput">The type of the symbol in the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting output</typeparam>
    internal class AsyncParser<TInput, TNode> : IParser<TInput, TNode>
    {
        readonly Func<IObservable<TInput>, IObserver<IParse<TInput, TNode>>, Task<IDisposable>> _ParseAsync;
        public AsyncParser(Func<IObservable<TInput>, IObserver<IParse<TInput, TNode>>, Task<IDisposable>> parseAsync)
        {
            if (parseAsync == null)
            {
                throw new ArgumentNullException("parseAsync");
            }
            _ParseAsync = parseAsync;
        }

        public IObservable<IParse<TInput,TNode>> Parse(IObservable<TInput> input)
        {
            return Observable.Create<IParse<TInput, TNode>>(
                subscribeAsync: observer => _ParseAsync(input, observer));
        }
    }
}
