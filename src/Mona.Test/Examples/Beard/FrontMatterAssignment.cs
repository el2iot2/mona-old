using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona.Test.Examples.Beard
{
    /// <summary>
    /// A "Node" type that captures the details of a front matter assignment
    /// </summary>
    public class FrontMatterAssignment
    {
        readonly IEnumerable<string> _PropertyChain;
        readonly object _Literal;
        public FrontMatterAssignment(IEnumerable<string> propertyChain, object literal)
        {
            _PropertyChain = propertyChain;
            _Literal = literal;
        }

        public IEnumerable<string> PropertyChain
        {
            get { return _PropertyChain; }
        }

        public object Literal
        {
            get { return _Literal; }
        }
    }        
}
