﻿using System;
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
                Strings.ErrorSinglePredicateFormat.Interpolate(
                    Strings.SymbolTypeCharacter,
                    Strings.PredicateUnspecified);
            return Single<char>(predicate: predicate, failureMessage: failureMessage);
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

        /// <summary>
        /// Creates a parser that expects the specified character
        /// </summary>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IParser<char, char> SingleChar(char expected)
        {
            return SingleChar(predicate: actual => actual == expected, 
                failureMessage: Strings.ErrorSinglePredicateFormat
                    .Interpolate(
                        Strings.SymbolTypeCharacter,
                        Strings.PredicateWasLiterallyFormat
                            .Interpolate(expected)));
        }

        /// <summary>
        /// Creates a parser that expects a single, unspecified char
        /// </summary>
        /// <returns></returns>
        public static IParser<char, char> SingleChar()
        {
            return Single<Char>(
                failureMessage: Strings.ErrorSingleFormat
                    .Interpolate(
                        Strings.SymbolTypeCharacter));
        }
    }
}