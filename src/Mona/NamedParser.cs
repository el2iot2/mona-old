using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// A wrapping parser that enables a name
    /// </summary>
    /// <typeparam name="TInput">The type of the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting tree node</typeparam>
    internal class NamedParser<TInput, TNode> : IParser<TInput, TNode>
    {
        readonly string _Name;
        readonly IParser<TInput, TNode> _Parser;
        public NamedParser(IParser<TInput, TNode> parser, string name)
        {
            _Parser = parser;
            _Name = name;
        }

        public IObservable<IParse<TInput,TNode>> Parse(IObservable<TInput> input)
        {
            return _Parser
                .Parse(input)
                .Select(parse => parse.Failed() ? 
                    new NamedParse<TInput, TNode>(parse, Name) : 
                    parse);
        }

        public string Name
        {
            get { return _Name; }
        }
    }
}
