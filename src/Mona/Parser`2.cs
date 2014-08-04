using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
    /// <summary>
    /// A simple wrapper around the parse func that makes up a parser
    /// </summary>
    /// <remarks>
    /// We make this a full type, rather than just a Func so that we can better manage
    /// Composite parsers
    /// </remarks>
    /// <typeparam name="TInput">The type of the symbol in the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting output</typeparam>
    internal class Parser<TInput, TNode> : IParser<TInput, TNode>
    {
        readonly Func<IEnumerable<TInput>, IParse<TInput, TNode>> _Parse;
        public Parser(Func<IEnumerable<TInput>, IParse<TInput, TNode>> parse)
        {
            if (parse == null)
            {
                throw new ArgumentNullException("parse");
            }
            _Parse = parse;
        }

        public IParse<TInput,TNode> Parse(IEnumerable<TInput> input)
        {
            return _Parse(input);
        }
    }
}
