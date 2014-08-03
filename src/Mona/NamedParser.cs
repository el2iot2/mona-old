using System;
using System.Collections.Generic;
using System.Linq;
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

        public IParse<TInput,TNode> Parse(IEnumerable<TInput> input)
        {
            var parse = _Parser
                .Parse(input);

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
