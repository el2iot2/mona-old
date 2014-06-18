using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    public static class Parseable
    {
        public static IParser<TSource, INode<TSource>> Single<TSource>(Func<TSource, bool> predicate, string expectation)
        {
            return new FuncParser<TSource, INode<TSource>>(
                input => input
                    .Take(1)
                    .Select(symbol => predicate(symbol) ?
                         new ObserverNode<TSource>(Enumerable.Repeat(symbol,1), input, null):
                         new ObserverNode<TSource>(Enumerable.Repeat(symbol,1), input, null))
            );
        }
    }
}
