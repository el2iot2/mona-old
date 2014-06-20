using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <typeparam name="TInput">The type of the symbol in the input</typeparam>
    /// <typeparam name="TNode">The type of the resulting output</typeparam>
    internal class Parser<TInput, TNode> : IParser<TInput, TNode>
    {
        readonly Func<IObservable<TInput>, IObservable<IParse<TInput,TNode>>> _ParseFunc;
        public Parser(Func<IObservable<TInput>, IObservable<IParse<TInput,TNode>>> parseFunc)
        {
            if (parseFunc == null)
            {
                throw new ArgumentNullException("parseFunc");
            }
            _ParseFunc = parseFunc;
        }

        public IObservable<IParse<TInput,TNode>> Parse(IObservable<TInput> input)
        {
            return _ParseFunc(input);
        }
    }
}
