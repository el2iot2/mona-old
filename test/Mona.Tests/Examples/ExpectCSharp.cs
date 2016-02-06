using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mona.Test.Examples
{
    /// <summary>
    /// Interpretation of the C# grammar listed at
    /// http://msdn.microsoft.com/en-us/library/aa664812(v=vs.71).aspx
    /// </summary>
    public static partial class ExpectCSharp
    {
        public static IParser<char, IEnumerable<char>> Whitespace()
        {
            //whitespace:
            //    Any character with Unicode class Zs
            //    Horizontal tab character (U+0009)
            //    Vertical tab character (U+000B)
            //    Form feed character (U+000C)
            return Expect.While<char>(symbol =>
            {
                switch (symbol)
                {
                    case '\u0009':
                    case '\u000B':
                    case '\u000C':
                        return true;
                    default:
                        return Char.GetUnicodeCategory(symbol) == UnicodeCategory.SpaceSeparator;
                }
            });
        }      
    }
}
