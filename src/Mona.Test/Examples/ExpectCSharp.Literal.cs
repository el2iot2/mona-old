using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.Test.Examples
{
    /// <summary>
    /// Interpretation of the C# grammar listed at
    /// http://msdn.microsoft.com/en-us/library/aa664812(v=vs.71).aspx
    /// </summary>
    public static partial class ExpectCSharp
    {
        public static IParser<char, object> Literal()
        {
            //null-literal:
            //  null
            var null_literal = Expect.String("null", chars => (object)null);

            //quote-escape-sequence:
            //  ""
            var quote_escape_sequence = Expect.String("\"\"");

            return null;
        }      
    }
}
