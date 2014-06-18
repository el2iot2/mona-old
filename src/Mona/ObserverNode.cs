using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    internal class ObserverNode<TSource> : Node<TSource>
    {
        readonly IEnumerable<TSource> _Observation;
        public ObserverNode(
            IEnumerable<TSource> observation,
            IObservable<TSource> remainder,
            Exception error
            )
            : base(remainder, error)
        {
            _Observation = observation;
        }

        public IEnumerable<TSource> Observation { get { return _Observation; } }

    }
}
