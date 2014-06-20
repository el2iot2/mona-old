using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// All basic parser generateors and extension methods for combining them
    /// </summary>
    public static partial class Parsers
    {
        /// <summary>
        /// Creates a parser that parses a single character with the specified predicate and failure message
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="failureMessage"></param>
        /// <returns></returns>
        public static IParser<char, char> SingleChar(Func<char, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorSingleSymbolPredicateFormat.Interpolate(
                    Strings.SymbolTypeCharacter,
                    Strings.PredicateUnspecified);
            return SingleSymbol<char>(predicate: predicate, failureMessage: failureMessage);
        }

        /// <summary>
        /// Creates a parser that parses a single character with the specified predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IParser<char, char> SingleChar(Func<char, bool> predicate)
        {
            return SingleChar(predicate: predicate, failureMessage: null);
        }
    }
}
