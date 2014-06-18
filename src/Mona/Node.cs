using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    internal class Node<TSource> : INode<TSource>
    {
        readonly IObservable<TSource> _Remainder;
        readonly Exception _Error;
        public Node(
            IObservable<TSource> remainder,
            Exception error
            )
        {
            _Remainder = remainder;
            _Error = error;
        }

        public IObservable<TSource> Remainder
        {
            get { return _Remainder; }
        }

        public Exception Error
        {
            get
            {
                return _Error;
            }
        }
    }
}
