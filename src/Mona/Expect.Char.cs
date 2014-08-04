using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mona
{
    public static partial class Expect
    {
        /// <summary>
        /// Creates a parser that parses a single character with the specified predicate and failure message
        /// </summary>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <param name="failureMessage"></param>
        /// <returns>The parser.</returns>
        public static IParser<char, char> Char(Func<char, bool> predicate, string failureMessage)
        {
            failureMessage = failureMessage ??
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeCharacter,
                    Strings.PredicateUnspecified);
            return Single<char>(predicate: predicate, failureMessage: failureMessage);
        }

        /// <summary>
        /// Creates a parser that parses a single character with the specified predicate
        /// </summary>
        /// <param name="predicate">A function to test each input symbol for a condition</param>
        /// <returns>The parser.</returns>
        public static IParser<char, char> Char(Func<char, bool> predicate)
        {
            return Char(predicate: predicate, failureMessage: null);
        }

        /// <summary>
        /// Creates a parser that expects the specified character
        /// </summary>
        /// <param name="expected"></param>
        /// <returns>The parser.</returns>
        public static IParser<char, char> Char(char expected)
        {
            return Char(predicate: actual => actual == expected, 
                failureMessage: Strings.ErrorSinglePredicateFormat
                    .Interpolate(
                        Strings.SymbolTypeCharacter,
                        Strings.PredicateWasLiterallyFormat
                            .Interpolate(expected)));
        }

        /// <summary>
        /// Creates a parser that expects a single, unspecified char
        /// </summary>
        /// <returns>The parser.</returns>
        public static IParser<char, char> Char()
        {
            return Single<Char>(
                failureMessage: Strings.ErrorSingleFormat
                    .Interpolate(
                        Strings.SymbolTypeCharacter));
        }

    }
}
