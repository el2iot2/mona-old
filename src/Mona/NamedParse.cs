using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// A wrapping parse that enables a name on the exception
    /// </summary>
    /// <typeparam name="TInput">The type of the input symbol</typeparam>
    /// <typeparam name="TNode">The type of the resulting tree node</typeparam>
    internal class NamedParse<TInput, TNode> : IParse<TInput, TNode>
    {
        readonly string _Name;
        readonly IParse<TInput, TNode> _Parse;
        public NamedParse(IParse<TInput, TNode> parse, string name)
        {
            _Parse = parse;
            _Name = name;
        }

        public TNode Node
        {
            get { return _Parse.Node; }
        }

        public IEnumerable<TInput> Remainder
        {
            get { return _Parse.Remainder; }
        }

        public Exception Error
        {
            get 
            {
                if (_Parse.Error == null)
                {
                    return null;
                }
                return new Exception(_Name, _Parse.Error);
            }
        }
    }
}
