using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    internal class FuncParser<TSource, TNode> : IParser<TSource, TNode>
        where TNode : INode<TSource>
    {
        readonly Func<IObservable<TSource>, IObservable<TNode>> _Parse;
        public FuncParser(Func<IObservable<TSource>, IObservable<TNode>> parse)
        {
            _Parse = parse;
        }

        public IObservable<TNode> Parse(IObservable<TSource> input)
        {
            return _Parse(input);
        }
    }
}
