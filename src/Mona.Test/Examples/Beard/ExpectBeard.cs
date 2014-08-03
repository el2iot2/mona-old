using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.Test.Examples.Beard
{
    /// <summary>
    /// A parser generator that expects beard templates
    /// </summary>
    public static class ExpectBeard
    {
        public static IParser<char, IEnumerable<string>> FrontMatterPropertyChain()
        {
            return null;
        }

        public static IParser<char, FrontMatterAssignment> FrontMatterAssignment()
        {
            return Expect.Concatenation(
                FrontMatterPropertyChain(),
                ExpectCSharp.Whitespace(),
                Expect.Char('='),
                ExpectCSharp.Whitespace(),
                ExpectCSharp.Literal(),
                (propertyChain, ws0, eq, ws1, literal) => new FrontMatterAssignment(propertyChain, literal)
                );
        }

        public static IParser<char, IEnumerable<FrontMatterAssignment>> FrontMatter()
        {
            return Expect.Concatenation(
                Expect.String("<!--"),
                ExpectCSharp.Whitespace(),
                Expect.ZeroOrMore(FrontMatterAssignment()),
                ExpectCSharp.Whitespace(),
                Expect.String("-->"),
                nodeSelector: (prefix, ws0, definitions, ws1, suffix) => definitions
            );
        }

        public static IParser<char, IEnumerable<FrontMatterAssignment>> OptionalFrontMatter()
        {
            return Expect.Optional(
                parser: FrontMatter(),
                nodeSelector: assignments => assignments ?? Enumerable.Empty<FrontMatterAssignment>());
        }
    }
}
