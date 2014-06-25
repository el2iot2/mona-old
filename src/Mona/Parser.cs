using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// A simple wrapper around the parse func that makes up a parser
    /// </summary>
    /// <remarks>
    /// We make this a full type, rather than just a Func so that we can better manage
    /// Composite parsers
    /// </remarks>
    /// <typeparam name="TInput">The type of the symbol in the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting output</typeparam>
    internal class Parser<TInput, TNode> : IParser<TInput, TNode>
    {
        readonly Func<IObservable<TInput>, IObserver<IParse<TInput, TNode>>, IDisposable> _Parse;
        public Parser(Func<IObservable<TInput>, IObserver<IParse<TInput, TNode>>, IDisposable> parse)
        {
            if (parse == null)
            {
                throw new ArgumentNullException("parse");
            }
            _Parse = parse;
        }

        public IObservable<IParse<TInput,TNode>> Parse(IObservable<TInput> input)
        {
            var delegatingInput = Observable.Create

            return Observable.Create<IParse<TInput, TNode>>(
                subscribe: observer => _Parse(input, observer));
        }
    }
}
