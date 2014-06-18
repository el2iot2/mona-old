using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    public interface IParser<TSource, TNode>
        where TNode : INode<TSource>
    {
        IObservable<TNode> Parse(IObservable<TSource> input);
    }
}
