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

        public async Task<IParse<TInput,TNode>> ParseAsync(IConnectableObservable<TInput> input)
        {
            var parse = await _Parser
                .ParseAsync(input);

            if (parse.Failed())
            {
                return new NamedParse<TInput, TNode>(parse, Name);
            }
            return parse;
        }

        public string Name
        {
            get { return _Name; }
        }
    }
}
