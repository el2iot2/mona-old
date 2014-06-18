using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    public static class Parseable
    {
        public static IParser<TSource, TNode> Single<TSource, TNode>(IObservable<TSource> input, Func<TSource, bool> predicate)
            where TNode : INode<TSource>
        {
            return new FuncParser<TSource, TNode>();
        }
    }
}
