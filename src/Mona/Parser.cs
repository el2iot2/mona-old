using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    public class Parser
    {
        public static IParser<TSource, TNode> Create<TSource, TNode>()
            where TNode : INode<TSource>
        {
            throw new NotImplementedException();
        }
    }

}
