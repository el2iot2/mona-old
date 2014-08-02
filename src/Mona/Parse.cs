using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// Represents the result of a parser's work
    /// </summary>
    /// <typeparam name="TInput">The type of the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting tree node</typeparam>
    internal class Parse<TInput, TNode> : IParse<TInput, TNode>
    {
        readonly TNode _Node;
        readonly IConnectableObservable<TInput> _Remainder;
        readonly Exception _Error;
        public Parse(
            TNode node,
            IConnectableObservable<TInput> remainder,
            Exception error
            )
        {
            _Node = node;
            _Remainder = remainder;
            _Error = error;
        }

        public TNode Node
        {
            get 
            {
                return _Node;
            }
        }

        /// <summary>
        /// The remaining Input, if any
        /// </summary>
        public IConnectableObservable<TInput> Remainder
        {
            get { return _Remainder; }
        }

        /// <summary>
        /// The exception, if one occurred. Null otherwise
        /// </summary>
        public Exception Error
        {
            get
            {
                return _Error;
            }
        }
    }
}
